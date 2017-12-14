using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP586Backend.Models;

namespace COMP586Backend
{
    //// DB Context for en entity framework. Lender database will contain transaction tables.
    //public class LedgerDBContext : DbContext
    //{
    //    // Sets the database to transaction types.
    //    public DbSet<ITransaction> Transactions { get; set; }

    //    public DbSet<ITransaction> Users { get; set; }

    //    //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //  {
    //    //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    //    //  }

    //    // Passes contect options to base class.
    //    public LedgerDBContext(DbContextOptions<LedgerDBContext> options) : base(options)
    //    {
    //    }


    //}
}
