using BankingControlPanel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //DB context and the the DB data sets for communication of DB tables
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
