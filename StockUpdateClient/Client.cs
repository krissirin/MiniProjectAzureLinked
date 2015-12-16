using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using StockUpdate.Models;
using StockUpdate.Controllers;

namespace StockViewClient
{
    class Client
    {
        static async Task GetAllStocks()     
        {
            try
            {
                using (HttpClient client = new HttpClient())                                            // Dispose() called autmatically in finally block
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");          // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            // or application/xml

                    // GET all
                    HttpResponseMessage response = await client.GetAsync("api/StocksAPI");           // async call, await suspends until task finished            
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var output = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
                        Console.WriteLine("\nAll Stocks: ");
                        foreach (var v in output)
                        {
                            Console.WriteLine("Stock Ref: {0} Ticker: {1} Stock Name: {2} Price: {3}",
                                v.StockReference, v.Ticker, v.StockName, v.Price);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        // add a stock
        static async Task AddStock(int stockRef, string tckr, string name, double price)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");                             // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // POST /api/stock with a listing serialised in request body
                    // create a new listing for SalesForce Inc, reference 20
                    Stock create = new Stock() { StockReference = stockRef, Ticker = tckr, StockName = name,  Price = price };

                    string path = "api/StocksAPI/" + stockRef.ToString() + "/";

                    HttpResponseMessage response = await client.PostAsJsonAsync(path, create); 
                    if (response.IsSuccessStatusCode)                                                  
                    {
                        Uri newStockUri = response.Headers.Location;
                        var newStock = await response.Content.ReadAsAsync<Stock>();
                        Console.WriteLine("\nStock Added: \nStock Ref: {0}, Ticker: {1} Name: {2} Price: {3}",
                            newStock.StockReference, newStock.Ticker, newStock.StockName, newStock.Price);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static async Task FindStockRef(string tckr)
        {
            try
            {
                using (HttpClient client = new HttpClient())                                            // Dispose() called autmatically in finally block
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");          // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            // or application/xml

                    // GET all
                    HttpResponseMessage response = await client.GetAsync("api/StocksAPI");           // async call, await suspends until task finished            
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var output = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;

                        int SearchResult = -1;
                        foreach (var v in output)
                        {
                            if (v.Ticker == tckr)
                            {
                                SearchResult = v.StockReference;
                            }
                        }
                        if(SearchResult != -1)
                        {
                            Console.WriteLine("\nStock Reference for ticker {0} is {1}", tckr, SearchResult);
                        }
                        else
                        {
                            Console.WriteLine("\nStock Reference not found");
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static async Task ReturnSingleStock(int stockRef)
        {
            try
            {
                using (HttpClient client = new HttpClient())                                            // Dispose() called autmatically in finally block
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");          // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            // or application/xml

                    string path = "api/StocksAPI/" + stockRef.ToString() + "/";
                    // GET api/StocksAPI/stockRef
                    HttpResponseMessage response = await client.GetAsync(path); 
                    // async call, await suspends until task finished            
                    if (response.IsSuccessStatusCode)
                    {
                        // read result 
                        var output = response.Content.ReadAsAsync<Stock>().Result;
                        Console.WriteLine("\nResult of Stock Search:\nStock Ref: {0}, Ticker: {1}, Name: {2}, Price: {3}",
                            output.StockReference, output.Ticker, output.StockName, output.Price);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        // update a stock listing 
        static async Task UpdateStock(int stockRef, string tckr, string name, double price)  
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");

                    client.DefaultRequestHeaders.
                       Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Stock create = new Stock() { StockReference = stockRef, Ticker = tckr, StockName = name, Price = price };

                    string path = "api/StocksAPI/" + stockRef.ToString() + "/";
                    
                    // update by Put to /api/StocksAPI/20 a listing serialised in request body
                    HttpResponseMessage response = await client.PutAsJsonAsync(path, create);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("\nUpdated Stock Details:\nStock Ref: {0}, Ticker: {1}, Name: {2}, Price: {3}",
                            create.StockReference, create.Ticker, create.StockName, create.Price);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // delete a stock listing
        static async Task DeleteStock(int stockRef)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");

                    client.DefaultRequestHeaders.
                       Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string path = "api/StocksAPI/" + stockRef.ToString() + "/";

                    // Delete/api/StocksAPI/stockRef                                                    
                    HttpResponseMessage response = await client.DeleteAsync(path);
                    if (response.IsSuccessStatusCode)
                    {
                        //Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                        var output = response.Content.ReadAsAsync<Stock>().Result;
                        Console.WriteLine("\nDeleted Stock Details:\nStock Ref: {0}, Ticker: {1}, Name: {2}, Price: {3}",
                            output.StockReference, output.Ticker, output.StockName, output.Price);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main()
        {
            //COMMENT OUT TASKS AND CHANGE DETAILS AS APPROPRIATE:

            //Task result = GetAllStocks();
            //result.Wait();

            //Task result1 = AddStock(25, "SFOR", "SalesForce Inc", 59.75);  //Salesforce stcok added    
            //result1.Wait();

            //Task result11 = FindStockRef("SFOR");  //Salesforce stock reference found from ticker    
            //result11.Wait();

            //Task result2 = ReturnSingleStock(26);      //Salesforce Stock returned  
            //result2.Wait();

            //Task result3 = UpdateStock(26, "SFOR", "SalesForce Inc", 55.34); //Salesforce price increased
            //result3.Wait();

            //Task result4 = DeleteStock(26); //Salesforce stock deleted
            //result4.Wait();

            Task result5 = GetAllStocks(); ; //All stocks returned again for inspection
            result5.Wait();

            Console.ReadLine();

        }
    }
}