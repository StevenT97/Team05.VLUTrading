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
using SEP_Demo.Admin.Controllers;

namespace SEP_Demo.Tests.Sprint02UnitTest
{
    [TestClass]
    public class ServiceControllerUnitTest
    {
        [TestMethod]
        public void IndexService()
        {
            // Arrange
            ServiceController controller = new ServiceController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
