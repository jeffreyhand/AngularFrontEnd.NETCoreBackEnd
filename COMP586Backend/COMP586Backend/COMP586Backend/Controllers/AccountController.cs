using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using COMP586Backend.Data;
using COMP586Backend.Models;
using System.Web.Http.Cors;

namespace COMP586Backend.Controllers
{

    // Class for Account to use which handles user email and password to allow registration.
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Identity Framework core creates a new user and generates a JSON Web Token (JWT) which is sent to the web api and stored in the browser's localstorage.
    /// The token will get passed in the authorization header of the all requests that sent from the front end to the back end.
    /// The back end can decode the token to retrieve information needed to idenify the saved user account.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {

        readonly CurrencyWalletContext m_context;

        readonly UserManager<IdentityUser> userManager;
        readonly SignInManager<IdentityUser> signInManager;


        // User manager
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CurrencyWalletContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;

            m_context = context;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {

            // Registration was a success.
            if (!m_context.Users.Any(x => x.Email == credentials.Email))
            {

                var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };

                // Embed user ID within the token itself.
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id)  // Subject is User ID.
                };

                string token = CreateToken(user);

                User newRegistreredUser = new User() { Token = user.Id, Email = credentials.Email, Password = credentials.Password };
                m_context.Users.Add(newRegistreredUser);

                // Asynchronous to handle multiple requests at a time.
                await m_context.SaveChangesAsync();   

                // 200 - OK response code along with generated token.
                return Ok(token);

            }
            else
            {
                return BadRequest("User Already Exists");
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {

            User existingUser = m_context.Users.SingleOrDefault(x => x.Email == credentials.Email && x.Password == credentials.Password);

            if (existingUser != null)
            {
                var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };

                string token = CreateToken(user);

                existingUser.Token = user.Id;
                await m_context.SaveChangesAsync();

                // 200 - OK response code along with generated token.
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }

        }


        string CreateToken(IdentityUser user)
        {
            var claims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, user.Id) };

            // Take in a secure sign-in key phrase usually stored as a secure config setting.
            // Signed with the same key used by the controller when it created the token.
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Create and return a JSON Web Token (JWT) with the sign in credentials.
            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

}
