using BankingControlPanel.Data;
using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;
using BankingControlPanel.Services;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanel.Extensions
{
    internal static class DataExtensions
    {
        #region Public Static Functions
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration config)
        {
            // Add identity services for ApplicationUser and IdentityRole using Entity Framework stores and default token providers
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Register ClientService with scoped lifetime
            services.AddScoped<IClientService, ClientService>();

            // Register AccountService with scoped lifetime
            services.AddScoped<IAccountService, AccountService>();

            // Return the service collection with the added services
            return services;
        }
        #endregion
    }
}
