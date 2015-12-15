using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockUpdate.Models
{
    public class Stock
    {
        [Key]
        public int StockReference { get; set; }
        [StringLength(20, MinimumLength = 1)]
        public String Ticker { get; set; }
        [StringLength(200, MinimumLength = 1)]
        public string StockName { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}