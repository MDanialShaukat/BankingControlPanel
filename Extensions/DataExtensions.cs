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
            //Add identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Data Layer Options
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
        #endregion
    }
}
