﻿using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExampleText
{
    public class AccountControllerTestFixture
    {
        [Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
            ]

        public void TestValidateEmail(string email, bool expectedResult)
        {
            //arrange: az első lépés mindig a teszteléshez szükséges elemek összegyűjtés és beállítása
            var accountController = new AccountController();

            //Act: a második a tesztelni kívánt tevékenység végrehajtása
            var actualResult = accountController.ValidateEmail(email);

            //Assert: a harmadik pedig az eredmények helyességének ellenőrzés. összehasonlítjuk az elvárt eredményt a tényleges eredményyel
            Assert.AreEqual(expectedResult, actualResult);
        }


        /*jelszóval kapcsolatos elvárások az accountController.cs-ben van leírva
         * "A jelszó legalább 8 karakter hosszú kell legyen, 
         * csak az angol ABC betűiből és számokból állhat, és 
         * tartalmaznia kell legalább egy kisbetűt, 
         * egy nagybetűt és egy számot.");*/

        [Test,
            TestCase("abc123",false),
            TestCase("ABCDABCD", false),
            TestCase("abcdabcd", false),
            TestCase("Abc1", false),
            TestCase("Abcabc11", true)
            ]

        public void TesztValidatePassword(string password, bool expectedResult)
        {
            //arrange: az első lépés mindig a teszteléshez szükséges elemek összegyűjtés és beállítása
            var accountController = new AccountController();

            //Act: a második a tesztelni kívánt tevékenység végrehajtása
            var actualResult = accountController.ValidatePassword(password);

            //Assert: a harmadik pedig az eredmények helyességének ellenőrzés. összehasonlítjuk az elvárt eredményt a tényleges eredményyel
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test,
            TestCase("info@uni-corvinus.hu", "Abcd1234"),
            TestCase("info@uni-corvinus.hu", "Abcd123456")
            ]

        public void TestRegisterHappyPath(string email, string password)
        {
            //arrange: az első lépés mindig a teszteléshez szükséges elemek összegyűjtés és beállítása
            var accountController = new AccountController();

            //Act: a második a tesztelni kívánt tevékenység végrehajtása
            var actualResult = accountController.Register(email, password);

            //Assert: a harmadik pedig az eredmények helyességének ellenőrzés. összehasonlítjuk az elvárt eredményt a tényleges eredményyel
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);

            //Guid.Empty: guid-nak mi az üres értéke. Itt ha üres, akkor probléma van:
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
            ]

        public void TestRegisterValidateException(string email, string password)
        {
            //arrange: az első lépés mindig a teszteléshez szükséges elemek összegyűjtés és beállítása
            var accountController = new AccountController();

            //Act: a második a tesztelni kívánt tevékenység végrehajtása
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.IsInstanceOf<ValidationException>(exception);
                //throw;
            }

            //Assert: a harmadik pedig az eredmények helyességének ellenőrzés. összehasonlítjuk az elvárt eredményt a tényleges eredményyel
        }
    }
}
