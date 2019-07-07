using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirplaneAPI;
using AirplaneAPI.Controllers;

namespace AirplaneAPI.Tests.Controllers
{
    [TestClass]
    public class RoutesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var controller = new RoutesController();

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }
    }
}
