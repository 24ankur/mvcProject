using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedLogic.ViewModels;
using UserInterface.Controllers;

namespace UnitTestBookEventManagement
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void SignUp()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.SignUp() as ViewResult;

            // Assert
            Assert.AreEqual("SignUp", result.ViewName);
            
        }

        [TestMethod]

        public void Login()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.AreEqual("Login", result.ViewName);

        }

        [TestMethod]

        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("About", result.ViewName);

        }
    }

}

