using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Identity
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {                
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Stores.ProtectPersonalData = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
