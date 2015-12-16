using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

// Stock model class
namespace StockUpdate.Models
{
     // an order for stock, with data annotations
    public class Stock
    {
        // all value types are impicility required
        [Key]
        public int StockReference { get; set; }     //PK
        
        [StringLength(20, MinimumLength = 1)]       //min 1 to max 20 chars
        public String Ticker { get; set; }
        
        [StringLength(200, MinimumLength = 1)]      //min 1 to max 200 chars
        public string StockName { get; set; }
        
        [DataType(DataType.Currency)]               //display $ 
        public double Price { get; set; }
    }
}
