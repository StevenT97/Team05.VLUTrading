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
   public class EditStatusUnitTest
    {
        [TestMethod]
        public void View_EditStatus()
        {
            // Arrange
            AdminsController controller = new AdminsController();
            // Act
            PartialViewResult result = controller.EditStatus(2) as PartialViewResult;
            // Assert
            Assert.AreEqual("EditStatusPartial", result.ViewName);
        }
        [TestMethod]
        public void EditStatus_Success()
        {
            // Arrange
            AdminsController controller = new AdminsController();
            // Act
            var pr = new Product
            {
                ID =2
                
            };
            var redirectToRouteResult = controller.EditStatus(pr, 4) as RedirectToRouteResult;
            //assert
            Assert.AreEqual("Listproduct", redirectToRouteResult.RouteValues["Action"]);
            Assert.AreEqual("Admins", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
