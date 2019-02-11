using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using CityService.Models;

namespace CityService.Controllers
{
	[EnableCors(origins: CORSConfig.allowedOrigins, headers: CORSConfig.allowedHeaders, methods: CORSConfig.allowedMethods, SupportsCredentials = true)]
	public class CitiesController : ApiController
    {
        private CityServiceContext db = new CityServiceContext();

        // GET: api/Cities1
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        // GET: api/Cities1/5
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> GetCity(int id)
        {
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCity(City[] cities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			foreach (var city in cities)
			{
				db.Entry(city).State = EntityState.Modified;
			}

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cities1
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> PostCity(City[] cities)
        {
			Dictionary<int, City> results = new Dictionary<int, City>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			foreach (var city in cities)
			{
				results.Add(city.CityID, city);
				db.Cities.Add(city);
			}
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", null, results);
        }

        // DELETE: api/Cities1/5
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> DeleteCity([FromUri] int[] ids)
        {
			foreach (var id in ids)
			{
				City city = await db.Cities.FindAsync(id);
				if (city == null)
				{
					return NotFound();
				}
				db.Cities.Remove(city);
			}
            await db.SaveChangesAsync();

			return StatusCode(HttpStatusCode.NoContent);
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.CityID == id) > 0;
        }
    }

	public static class CORSConfig

	{

		public const string allowedOrigins = "http://localhost:4200";

		public const string allowedHeaders = "*";

		public const string allowedMethods = "*";

	}
}