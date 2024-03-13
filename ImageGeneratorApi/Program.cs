using ImageGeneratorApi.Core.Application.Interfaces;
using ImageGeneratorApi.Core.Application.Services;
using ImageGeneratorApi.Domain.Entities;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

    // opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    // {
    //     In = ParameterLocation.Header,
    //     Description = "Please enter a valid token",
    //     Name = "Authorization",
    //     Type = SecuritySchemeType.Http,
    //     BearerFormat = "JWT",
    //     Scheme = "Bearer"
    // });
    // opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    // {
    //     {
    //         new OpenApiSecurityScheme
    //         {
    //             Reference = new OpenApiReference
    //             {
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             }
    //         },
    //         //TODO: Implement later roles or scopes required to access endpoints
    //         new string[] { }
    //     }
    // });
});

// Add authentication
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

//Add authorization
builder.Services.AddAuthorizationBuilder();

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlite(connectionString));


#region Service Injected
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddTransient<IDateTime, DateTimeService>();
#endregion

var app = builder.Build();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Seed database
await IdentityDbSeed.EnsureSeedData(app.Services);

app.Run();