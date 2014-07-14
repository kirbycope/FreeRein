using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FreeRein;
using FreeRein.Controllers;
using FreeRein.Models;

namespace FreeRein.Tests
{
    [TestClass]
    public class NarrativeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            NarrativeController controller = new NarrativeController();

            // Act
            ViewResult result = controller.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
