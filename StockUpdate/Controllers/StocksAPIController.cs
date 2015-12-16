using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using StockUpdate.Models;

namespace StockUpdate.Controllers
{
    public class StocksAPIController : ApiController
    {
        private StockUpdateContext db = new StockUpdateContext();

        // GET: api/StocksAPI
        public IQueryable<Stock> GetStocks()
        {
            return db.Stocks;
        }

        // GET: api/StocksAPI/5
        [ResponseType(typeof(Stock))]
        public async Task<IHttpActionResult> GetStock(int id)
        {
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // PUT: api/StocksAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStock(int id, Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.StockReference)
            {
                return BadRequest();
            }

            db.Entry(stock).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StocksAPI
        [ResponseType(typeof(Stock))]
        public async Task<IHttpActionResult> PostStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stocks.Add(stock);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = stock.StockReference }, stock);
        }

        // DELETE: api/StocksAPI/5
        [ResponseType(typeof(Stock))]
        public async Task<IHttpActionResult> DeleteStock(int id)
        {
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            db.Stocks.Remove(stock);
            await db.SaveChangesAsync();

            return Ok(stock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(int id)
        {
            return db.Stocks.Count(e => e.StockReference == id) > 0;
        }
    }
}