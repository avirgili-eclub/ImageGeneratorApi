using ImageGeneratorApi.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGeneratorApi.Infrastructure.Persistence;

public abstract class IdentityDbSeed
{
    //TODO: mejorar el Seed.
    private static async Task InitializeDatabase(IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        IdentityResult roleResult;
        // Crear roles si no existen
        var adminRoleExists = await roleManager.RoleExistsAsync("admin");
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }
        
        var userRoleExists = await roleManager.RoleExistsAsync("user");
        if (!userRoleExists)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole("user"));
        }

        // Crear usuario admin si no existe
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = true,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = "admin",
                TwoFactorEnabled = false
            };
            await userManager.CreateAsync(adminUser, "Admin123@");

            // Asignar rol admin al usuario admin
            var addRoleResult = await userManager.AddToRoleAsync(adminUser, "admin");
            Console.WriteLine(addRoleResult.Succeeded);
        }
    }
    
    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await InitializeDatabase(scope);
    }
}