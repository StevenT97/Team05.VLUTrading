using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;

namespace SEP_Demo.Tests
{


    [TestClass]
    public class AccountUnitTest
    {
        [TestMethod]
        public void ValidateLogin_EmailIDPassword()
        {
            
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var Username = "sonnguyen66@vanlanguni.vn";
            var password = "123123";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            
            var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            //
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("Index", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Home", redirectRoute.RouteValues["controller"]);
        }
        [TestMethod]
        public void ValidateLogin_WithInValidPassword()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var Username = "sonnguyen66@vanlanguni.vn";
            var password = "123456";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            //Assert
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("LogIn", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Account", redirectRoute.RouteValues["controller"]);
        }
        [TestMethod]
        public void validateLogIn_withInvalidEmailID()
        {
            //Arrange
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            var controller = new Controllers.AccountController();
            var Username = "";
            var password = "";
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            //act
            var redirectRoute = controller.Login(Username, password) as RedirectToRouteResult;
            //Assert
            Assert.IsNotNull(redirectRoute);
            Assert.AreEqual("LogIn", redirectRoute.RouteValues["action"]);
            Assert.AreEqual("Account", redirectRoute.RouteValues["controller"]);
            Assert.AreEqual("Login", redirectRoute.RouteName);
            Assert.AreEqual("* Account Invalid", redirectRoute.RouteName);
        }
        [TestMethod]
        public void LogIn()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        //[TestMethod]
        //public void TestLogInForRedirect()
        //{
        //    //arrange
        //    AccountController controller = new AccountController();
        //    var loginmodel = new User
        //    {
        //        EmailID = "sonnguyen66@vanlanguni.vn",
        //        Password = "123123",
        //    };
            
        //   // var EmailID = "sonnguyen66@vanlanguni.vn";
        //   // var Password = "123123";
        //    //act
        //    var result = controller.Login("sonnguyen66@vanlanguni.vn", "123123") as RedirectToRouteResult;
        //    // Assert
        //    Assert.AreEqual("Index", result.RouteValues["action"]);
        //    Assert.AreEqual("Home", result.RouteValues["controller"]);

        //}
        [TestMethod]
        public void SignUp()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.SignUp() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ForgetPassword()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.ForgetPassword() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ChangePassword()
        {
            //arrange
            AccountController controller = new AccountController();
            var EmailID = "sonnguyen66@vanlanguni.vn";
            var Password = "123123";
            // Act
            var result = controller.Login(EmailID, Password) as RedirectToRouteResult;
            ViewResult result1 = controller.ChangePassword() as ViewResult;
            //  Assert
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void VeriffyAccount()
        //{
        //    // Arrange
        //    AccountController controller = new AccountController();

        //    // Act
        //    ViewResult result = controller.VerifyAccount() as ViewResult;

        //    // Assert

        //    Assert.IsNotNull(result);
        //}
        [TestMethod]
        public void LogOut()
        {
            AccountController controller = new AccountController();
            var redirectToRouteResult = controller.LogOut() as RedirectToRouteResult;
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Home", redirectToRouteResult.RouteValues["controller"]);

            //// Arrange
            //AccountController controller = new AccountController();
            //// Act
            //var result1 = controller.Login("sonnguyen66@vanlanguni.vn", "123123") as RedirectToRouteResult;
            //// Act
            //ViewResult result = controller.LogOut() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}
