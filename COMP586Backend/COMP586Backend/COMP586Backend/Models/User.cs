using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP586Backend.Models
{
    public class User : IUser
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }      // User identification passed in the token to verify user is on client browser.

        public ICollection<Wallet> Wallets { get; set; }
    }

    public interface IUser
    {
        int UserID { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Token { get; set; }

        ICollection<Wallet> Wallets { get; set; }
    }
}
