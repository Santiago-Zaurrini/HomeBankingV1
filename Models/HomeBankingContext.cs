using Microsoft.EntityFrameworkCore;

namespace HomeBanking.Models
{
    public class HomeBankingContext : DbContext
    {
        public HomeBankingContext(DbContextOptions<HomeBankingContext> options) : base(options) { }

        //dbsets
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
