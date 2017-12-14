using COMP586Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP586Backend.Models;
using COMP586Backend.Controllers;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class ControllerUnitTest
    {
    
        WalletsController _SUT;
        ICurrencyWalletContext _currencyWalletInterface;

        DbContextOptionsBuilder<CurrencyWalletContext> CurrencyWalletContextOptionsBuilder = new DbContextOptionsBuilder<CurrencyWalletContext>();

        [TestInitialize]
        public void Setup()
        {
            
            CurrencyWalletContextOptionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Comp586DigitalCurrencyDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            _currencyWalletInterface = new CurrencyWalletContext(CurrencyWalletContextOptionsBuilder.Options);
            _SUT = new WalletsController((CurrencyWalletContext)_currencyWalletInterface);

        }

        [TestMethod]
        public async Task Add_User_Verify_Users_In_User_Table()
        {
            //arrange
            var context = new CurrencyWalletContext(CurrencyWalletContextOptionsBuilder.Options);
            string existingUserEmail = "test@test123.com";  // Seeded into database upon initialization.
            string newUserEmail = "test@testuser.com";


            // act
            var c = context.Users.Add(new User() { Email = newUserEmail });
            bool foundNewUser = await context.Users.AnyAsync(x => x.Email == newUserEmail);
            bool foundExistingUser = await context.Users.AnyAsync(x => x.Email == existingUserEmail);

            //assert
            Assert.IsFalse(foundNewUser);    // Database not synced, do not save.
            Assert.IsTrue(foundExistingUser);    // Saved previously from initial seed.
        }

    }
}
