using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace COMP586Backend.Models
{
    /// <summary>
    /// Type of object passed though JSON from front-end.
    /// Represents the individual transactions that make up the wallet or ledger.
    /// </summary>
    public class Transaction : ITransaction
    {
        public int TransactionID { get; set; }
        public int WalletID { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public DateTime PostDate { get; set; }

        [NotMapped]
        public string WalletTitle { get; set; }    // Only used to get string dropdown option value from front-end
    }


    public interface ITransaction
    {
        int TransactionID { get; set; }
        int WalletID { get; set; }
        double Quantity { get; set; }
        double Price { get; set; }
        DateTime PostDate { get; set; }

        string WalletTitle { get; set; }    // Only used to get string dropdown option value from front-end
    }

  }
