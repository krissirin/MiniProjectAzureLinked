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
        static async Task RunAsync()     // async methods return Task or Task<T>
        {
            try
            {
                using (HttpClient client = new HttpClient())                                            // Dispose() called autmatically in finally block
                {
                    client.BaseAddress = new Uri("http://stockupdate101.azurewebsites.net/");                             // base URL for API Controller i.e. RESTFul service

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
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                    ////GET Carparks in an Area e.g. D16
                    //try
                    //{
                    //    response = await client.GetAsync("carpark/area/D16");

                    //    response.EnsureSuccessStatusCode();                         // throw exception if not success
                    //    var output = response.Content.ReadAsAsync<IEnumerable<StockDetail>>().Result;
                    //    Console.WriteLine("\nCarParks in D16:");

                    //    foreach (var v in output)
                    //    {
                    //        if (v != null)
                    //        {
                    //            Console.WriteLine("Eircode: {0} Spaces: {1} Open: {2} Price/Hr: {3} Location: {4}",
                    //                v.Eircode, v.FreeSpaces, v.OpenClosed, v.PricePerHr, v.Location);
                    //        }
                    //    }
                    //}
                    //catch (HttpRequestException e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //}

                    ////GET Carparks in D16, if space available & carpark open - in order of cheapest first
                    //try
                    //{
                    //    response = await client.GetAsync("carpark/availableparking/D16");

                    //    response.EnsureSuccessStatusCode();                         // throw exception if not success
                    //    var posts = await response.Content.ReadAsAsync<IEnumerable<CarParkClass>>();
                    //    Console.WriteLine("\nCarParks in D16 that are open and have space available."
                    //            + "\nIn order of cheapest price first: ");
                    //    foreach (var v in posts)
                    //    {
                    //        Console.WriteLine("Eircode: {0} Spaces: {1} Open: {2} Price/Hr: {3} Location: {4}",
                    //            v.Eircode, v.FreeSpaces, v.OpenClosed, v.PricePerHr, v.Location);
                    //    }
                    //}
                    //catch (HttpRequestException e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // kick off
        static void Main()
        {
            Task result = RunAsync();               // convention is for async methods to finish in Async
            result.Wait();                          // block, not the same as await

            Console.ReadLine();
        }
    }
}