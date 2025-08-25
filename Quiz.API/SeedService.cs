using Microsoft.AspNetCore.Identity;
using Quiz.DAL.EF;
using Quiz.Shared.Models;
using System.Threading.Tasks;

namespace Quiz.API
{
    internal class Seeder
    {
        IServiceProvider _serviceProvider;

        public Seeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedDatabase()
        {
            using var scope = _serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Seeder>>();

            var roles = new List<string>() { "Admin", "User" };

            try
            {
                // Seeding default roles
                logger.LogInformation("Seeding roles...");
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new Role(role));
                    }
                }

                // Seeding default user (admin)
                logger.LogInformation("Seeding admin user...");
                var adminEmail = "admin@quiz.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new User(adminEmail);
                    await userManager.CreateAsync(admin);
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occured during the seeding process");
            }
        }
    }
}
