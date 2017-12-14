using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMP586Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using COMP586Backend.Data;
using System.Web.Http.Cors;

namespace COMP586Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        readonly CurrencyWalletContext m_context;

        // Inject the database context in the constructor (Constructor Injection).
        public TransactionsController(CurrencyWalletContext context)
        {
            m_context = context;
        }


        // GET API values to send to the front-end.
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            // Only show transactions that belong to that user.

            var userIdToken = HttpContext.User.Claims.First().Value;

            User user = m_context.Users.SingleOrDefault(x => x.Token == userIdToken);

            List<Wallet> wallets = m_context.Wallets.Where(x => x.UserID == user.UserID).ToList();

            List<int> walletIDs = new List<int>();

            // Create list of Wallet IDs to check against.
            foreach (var w in wallets)
            {
                walletIDs.Add(w.WalletID);
            }

            IEnumerable<Transaction> transactions = m_context.Transactions.Where(t => walletIDs.Contains(t.WalletID));

            // Pass in Wallet Name to transactions as strings.

            foreach (var t in transactions)
            {
                t.WalletTitle = m_context.Wallets.SingleOrDefault(x => x.WalletID == t.WalletID).Title;
            }

            return transactions;

        }


        // GET api
        [HttpGet("{id}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Transaction> Get(int id)
        {
            // Only show transactions that belong to that user.

            var userIdToken = HttpContext.User.Claims.First().Value;

            User user = m_context.Users.SingleOrDefault(x => x.Token == userIdToken);

            List<Wallet> wallets = m_context.Wallets.Where(x => x.UserID == user.UserID).ToList();

            List<int> walletIDs = new List<int>();

            foreach (var w in wallets)
            {
                walletIDs.Add(w.WalletID);
            }

            return m_context.Transactions.Where(t => walletIDs.Contains(t.WalletID));

        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]    // Added authorization decorator to enforce JWT check.
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Transaction transaction)
        {


            // Guard clause.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdToken = HttpContext.User.Claims.First().Value;

            User user = m_context.Users.SingleOrDefault(x => x.Token == userIdToken);

            // Find the selected wallet from the dropdown.
            Wallet wallet = m_context.Wallets.SingleOrDefault(x => x.UserID == user.UserID && x.Title == transaction.WalletTitle);
            transaction.PostDate = DateTime.Now;
            transaction.WalletID = wallet.WalletID;

            m_context.Transactions.Add(transaction);
            await m_context.SaveChangesAsync();    // Asynchronous to handle multiple requests at a time.
            return Ok(transaction);

        }




        // Can explicitly specify the transaction with the ID you want to modify in the URL
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Transaction transaction)
        {

            // Confirm that the ID being sent with the PUT data matches the transaction ID in the model.
            if (id != transaction.TransactionID)
            {
                return BadRequest();
            }

            // Notify context that the transaction has been modified and to update model.
            // Uses the id from the model itself.
            m_context.Entry(transaction).State = EntityState.Modified;

            await m_context.SaveChangesAsync();   // Asynchronous to handle multiple requests at a time.

            return Ok(transaction);
        }
    }
}