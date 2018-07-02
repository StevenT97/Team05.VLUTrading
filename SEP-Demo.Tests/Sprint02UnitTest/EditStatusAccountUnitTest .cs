using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEP_Demo.Controllers;
using System.Web.Mvc;
using SEP_Demo.Models;
using Moq;
using System.Web.Routing;
using System.Threading.Tasks;
using log4net;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;
using Newtonsoft.Json;
using SEP_Demo.Admin.Controllers;

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
   public class EditStatusAccountUnitTest
    {
        [TestMethod]
        public void View_EditStatusAccount()
        {
            // Arrange
            AdminsController controller = new AdminsController();
            // Act
            PartialViewResult result = controller.EditStatusAccount(6) as PartialViewResult;
            // Assert
            Assert.AreEqual("EditStatusAccountPartial", result.ViewName);
        }
        [TestMethod]
        public void EditStatusAccount_Success()
        {
            // Arrange
            AdminsController controller = new AdminsController();
            // Act
            
            var pr = new User
            {
                Id =42,
                ConfirmPassword = "TPjFdtSRSioqWM8jDGPQqEuRwby23sDJU3dVC3aWWEQ ="
            };
            var redirectToRouteResult = controller.EditStatusAccount(pr, 3) as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Admins", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
