using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockUpdate;
using StockUpdate.Controllers;
using StockUpdate.Models;
using System.Threading.Tasks;

namespace StockUpdate.Tests.Controllers
{
    [TestClass]
    public class StockControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            StocksController controller = new StocksController();

            var result = controller.Index() as Task<ActionResult>;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            //Arrange
            StocksController controller = new StocksController();
            //Act
           var result = controller.Details(2) as Task<ActionResult>;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            //Arrange
            StocksController controller = new StocksController();
            //Act
           var result = controller.Create() as ViewResult;
           // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Edit()
        {
            //Arrange
            StocksController controller = new StocksController();
            //Act
            var result = controller.Edit(1) as Task<ActionResult>;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Delete()
        {
            //Arrange
            StocksController controller = new StocksController();
            //Act
            var result = controller.Delete(1) as Task<ActionResult>;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
