using System.Text;
using ImageGeneratorApi.Core;
using ImageGeneratorApi.Core.StableDiffusion.Services;
using ImageGeneratorApi.Domain.Common.Entities;
using ImageGeneratorApi.Infrastructure.Clients;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Services;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();
builder.Services.AddLogging();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Image Generator API", Version = "v1",
        Description = "API to generate images from text and templates with AI."
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            //TODO: Implement later roles or scopes required to access endpoints
            new string[] { }
        }
    });
});

//TODO: obtain from a more secure place like Azure Key Vault or aws secret manager
var secretKey = builder.Configuration["JwtSettings:SecretKey"];

// Add authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "UMBRELLA",
            ValidAudience = "http://localhost:5228", // for future reference: should match the 'audience' parameter in JwtSecurityToken
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
    });

//Builder para manejar distintos tipos de autorizaciones si se requiere
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("BearerPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlite(connectionString));


#region Service Injected
builder.Services.AddCoreServices();
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddTransient<IDateTime, DateTimeService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services
    .AddRefitClient<IStableDifussionClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://127.0.0.1:7860"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Configure method in Startup.cs

#endregion

var app = builder.Build();

// Instantiate DatabaseImageGenerationStorage
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Now you can use dbContext within this scope
    var imageGenerationStorage = new DatabaseImageGenerationStorage(dbContext);
    // Use imageGenerationStorage
}


app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers()/*.RequireAuthorization("BearerPolicy")*/;
});
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
// app.Use(async (_, next) =>
// {
//     var mvcOptions = app.Services.GetRequiredService<MvcOptions>();
//     mvcOptions.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
//     await next.Invoke();
// });

// Seed database
await IdentityDbSeed.EnsureSeedData(app.Services);

app.Run();