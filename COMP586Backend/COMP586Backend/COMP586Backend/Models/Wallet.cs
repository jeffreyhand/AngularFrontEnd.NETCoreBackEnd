using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP586Backend.Models
{
    /// <summary>
    /// Ledger of transactions. There is a one-to-many relationship from Users to Wallet and a one-to-many relationship from Wallet to Transactions.
    /// </summary>
    public class Wallet : IWallet
    {
        public int WalletID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }


    public interface IWallet
    {
        int WalletID { get; set; }
        int UserID { get; set; }
        string Title { get; set; }

        ICollection<Transaction> Transactions { get; set; }
    }
}
