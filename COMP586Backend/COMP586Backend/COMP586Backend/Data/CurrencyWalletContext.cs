using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using COMP586Backend.Models;

namespace COMP586Backend.Data
{

    /// <summary>
    /// Helps Entity Framework create a schema.
    /// </summary>
    public class CurrencyWalletContext : DbContext, ICurrencyWalletContext
    {
        public CurrencyWalletContext(DbContextOptions<CurrencyWalletContext> options) : base(options)
        {

        }

        // There is a one-to-many relationship from Users to Wallet and a one-to-many relationship from Wallet to Transactions.
        // Entity Framework automatically detects the IDs and assignes the foriegn keys respectively.
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // It is a convention to use singular for table names, so I override that here. Otherwise it defaults to the plural.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().ToTable("Wallet");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }
    }

    public interface ICurrencyWalletContext
    {
        DbSet<Wallet> Wallets { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }
}
