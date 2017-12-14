using COMP586Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP586Backend.Data
{

    /// <summary>
    /// Seeds the database with text data that can be unit tested.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(CurrencyWalletContext context)
        {
            context.Database.EnsureCreated();

            // If there are any users, then the database has already been seeded.
            if (context.Users.Any())
            {
                return; 
            }

            var users = new User[]
            {
                new User{ Token = "sdf-345sdv-xdg", Email="test@test123.com", Password = "Test&password1"},
            };
            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();


            var wallets = new Wallet[]
            {
                new Wallet{ UserID=0, WalletID = 0, Title = "Test Wallet"},
            };
            foreach (Wallet wallet in wallets)
            {
                context.Wallets.Add(wallet);
            }
            context.SaveChanges();


            var transactions = new Transaction[]
            {
                new Transaction{ PostDate = DateTime.Now, WalletID = 0, Quantity=3, Price = 5433.56},
                new Transaction{ PostDate = DateTime.Now.AddMinutes(3), WalletID = 0, Quantity=2.1, Price = 2433.56}
            };
            foreach (Transaction t in transactions)
            {
                context.Transactions.Add(t);
            }
            context.SaveChanges();
        }
    }
}
