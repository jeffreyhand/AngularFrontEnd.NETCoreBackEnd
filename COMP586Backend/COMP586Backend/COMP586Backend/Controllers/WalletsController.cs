using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMP586Backend.Data;
using Microsoft.AspNetCore.Authorization;
using COMP586Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;

namespace COMP586Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Produces("application/json")]
    [Route("api/Wallets")]
    public class WalletsController : Controller
    {

        readonly CurrencyWalletContext m_context;

        // Inject the database context in the constructor (Constructor Injection).
        public WalletsController(CurrencyWalletContext context)
        {
            m_context = context;
        }


        // GET API/values to send to the front-end.
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]
        [HttpGet]
        public IEnumerable<Wallet> Get()
        {
            // Only show wallets that belong to that user.
            var userIdToken = HttpContext.User.Claims.First().Value;

            User user = m_context.Users.SingleOrDefault(x => x.Token == userIdToken);

            return m_context.Wallets.Where(x => x.UserID == user.UserID);

        }

        // Added authorization decorator to enforce JWT check.
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Wallet wallet)
        {
            // Guard clause.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdToken = HttpContext.User.Claims.First().Value;

            User user = m_context.Users.SingleOrDefault(x => x.Token == userIdToken);

            wallet.UserID = user.UserID;
            
            m_context.Wallets.Add(wallet);
            await m_context.SaveChangesAsync();    // Asynchronous to handle multiple requests at a time.
            return Ok(wallet);

        }


        // Can explicitly specify the wallet with the ID you want to modify in the URL
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Wallet wallet)
        {

            // Confirm that the ID being sent with the PUT data matches the transaction ID in the model.
            if (id != wallet.WalletID)
            {
                return BadRequest();
            }

            // Notify context that the wallet has been modified and to update model.
             m_context.Entry(wallet).State = EntityState.Modified;

            await m_context.SaveChangesAsync();   // Asynchronous to handle multiple requests at a time.

            return Ok(wallet);
        }
    }
}