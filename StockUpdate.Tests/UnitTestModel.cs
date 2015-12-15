using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockUpdate.Models;

namespace StockUpdate.Tests
{
    [TestClass]
    public class UnitTestModel
    {
        [TestMethod]
        public void TestStock()
        {
            Stock s = new Stock()
            {
                StockReference = 0,
                Ticker = "AAPL",
                StockName = "Apple Inc.",
                Price = 125.59
            };
            Assert.AreEqual(0, s.StockReference);
            Assert.AreEqual("AAPL", s.Ticker);
            Assert.AreEqual("Apple Inc.", s.StockName);
            Assert.AreEqual(125.59, s.Price);
        }
    }
}
