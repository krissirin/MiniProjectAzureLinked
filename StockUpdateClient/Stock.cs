using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockUpdateClient
{
    class Stock
    {
        public int StockReference { get; set; }
        public String Ticker { get; set; }
        public string StockName { get; set; }
        public double Price { get; set; }
    }
}
