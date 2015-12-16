using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using StockUpdate.Controllers;
using System.Threading.Tasks;
using System.Web.Http;
using StockUpdate.Models;
using System.Linq;

namespace StockUpdate.Tests.Controllers
{
    [TestClass]
    public class StockAPIControllerTest
    {
        [TestMethod]
        public void GetStocks()
        {
            // Arrange
            StocksAPIController controller = new StocksAPIController();

            var result = controller.GetStocks() as IQueryable<Stock>;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStock()
        {
            //Arrange
            StocksAPIController controller = new StocksAPIController();
            //Act
            var result = controller.GetStock(2) as Task<IHttpActionResult>;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PutStock()
        {
            //Arrange
            StocksAPIController controller = new StocksAPIController();
            Stock s = new Stock() { StockReference = 9, Ticker = "MSFT", StockName = "MicroSoft Inc", Price = 56.34 };
            //Act
            var result = controller.PutStock(6, s) as Task<IHttpActionResult>;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void PostStock()
        {
            //Arrange
            StocksAPIController controller = new StocksAPIController();
            Stock s = new Stock() { StockReference = 9, Ticker = "MSFT", StockName = "MicroSoft Inc", Price = 56.34 };
            //Act
            var result = controller.PostStock(s) as Task<IHttpActionResult>;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DeleteStock()
        {
            //Arrange
            StocksAPIController controller = new StocksAPIController();
            //Act
            var result = controller.DeleteStock(1) as IHttpActionResult;
            // Assert
            Assert.IsNull(result);
        }
    }
}