using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExampleText
{
    public class AccountControllerTestFixture
    {
        [Test]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //arrange: az első lépés mindig a teszteléshez szükséges elemek összegyűjtés és beállítása
            var accountController = new AccountController();

            //Act: a második a tesztelni kívánt tevékenység végrehajtása
            var actualResult = accountController.ValidateEmail(email);

            //Assert: a harmadik pedig az eredmények helyességének ellenőrzés
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
