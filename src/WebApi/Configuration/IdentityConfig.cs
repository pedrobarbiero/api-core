using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Data;
using WebApi.Extensions;

namespace WebApi.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddErrorDescriber<IdentityPortugueseMessages>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
