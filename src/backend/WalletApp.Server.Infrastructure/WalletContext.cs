using Microsoft.EntityFrameworkCore;
using WalletApp.Server.Core.Models;

namespace WalletApp.Server.Infrastructure
{
    public class WalletContext : DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; } = null!;
    }
}
