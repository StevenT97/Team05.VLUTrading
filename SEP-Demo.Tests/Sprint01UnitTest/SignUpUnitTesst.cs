using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Web;
using System.Net;

namespace SEP_Demo.Tests
{
    [TestClass]
    public class SignUpUnitTesst
    {
        //   Test SingUp
        [TestMethod]
        public void View_SignUp()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            ViewResult result = controller.SignUp() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void SingUp_withExistEmailID()
        {
            AccountController controller = new AccountController();
            var singup = new User
            {
                FirstName = "nguyen",
                LastName = "son",
                DateOfBirth = DateTime.Parse("Jan ,01, 2009"),
                EmailID = "thuannguyen444@vanlanguni.vn",
                Password = "456456456",
                ConfirmPassword = "456456456"
            };
            
            var result = controller.SignUp(singup) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        // Not Pass
        [TestMethod]
        public void SignUp_Success()
        {
            AccountController controller = new AccountController();
            var singup = new User
            {
                FirstName = "dinh",
                LastName = "tin",
                DateOfBirth = DateTime.Parse("Jan ,01, 2009"),
                EmailID = "tindinh1@vanlanguni.vn",
                Password = "456456456",
                ConfirmPassword = "456456456"
            };
            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
           
            context.Setup(expression: x => x.Request.Url).Returns(new Uri("http://localhost:62478/Account/VerifyAccount/3511a36e-77dc-43a1-880b-a0b7b18e9b24"));

            var result = controller.SignUp(singup) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
    }
}
