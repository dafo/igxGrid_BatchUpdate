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

        // GET: api/Cities
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        // GET: api/Cities/5
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

		// Comment the PostCity() method if you want to process the transactions on the client side

		// POST: api/Cities/UpdateCities
		public IHttpActionResult PostCity(ITransaction<City>[] transactions)
		{
			var addedCities = new Dictionary<int, City>();

			foreach (var transaction in transactions)
			{
				var city = db.Cities.Where(x => x.CityID == transaction.id).FirstOrDefault();
				switch (transaction.type)
				{
					case "update":

						// Update the properties values
						db.Entry(city).CurrentValues.SetValues(transaction.newValue);

						// You can set the values property by property instead of using SetValues()

						//city.CityName = transaction.newValue.CityName;
						//city.Population = transaction.newValue.Population;
						//city.TrainStation = transaction.newValue.TrainStation;
						//city.HolidayDate = transaction.newValue.HolidayDate;
						//city.Description = transaction.newValue.Description;
						break;
					case "add":
						City addedCity = db.Cities.Add(transaction.newValue);
						addedCities[addedCity.CityID] = addedCity;
						break;
					case "delete":
						db.Cities.Remove(city);
						break;

				}
			}
			try
			{
				db.SaveChanges();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok(addedCities);
		}

		// Uncomment the following block if you want to process the transactions on the client side

		//      // PUT: api/Cities/5
		//      [ResponseType(typeof(void))]
		//      public async Task<IHttpActionResult> PutCity(City[] cities)
		//      {
		//          if (!ModelState.IsValid)
		//          {
		//              return BadRequest(ModelState);
		//          }
		//	foreach (var city in cities)
		//	{
		//		db.Entry(city).State = EntityState.Modified;
		//	}

		//          await db.SaveChangesAsync();

		//          return StatusCode(HttpStatusCode.NoContent);
		//      }

		//      // POST: api/Cities
		//      [ResponseType(typeof(City))]
		//      public async Task<IHttpActionResult> PostCity(City[] cities)
		//      {
		//	Dictionary<int, City> results = new Dictionary<int, City>();
		//          if (!ModelState.IsValid)
		//          {
		//              return BadRequest(ModelState);
		//          }
		//	foreach (var city in cities)
		//	{
		//		results.Add(city.CityID, city);
		//		db.Cities.Add(city);
		//	}
		//          await db.SaveChangesAsync();

		//          return CreatedAtRoute("DefaultApi", null, results);
		//      }

		//      // DELETE: api/Cities/5
		//      [ResponseType(typeof(City))]
		//      public async Task<IHttpActionResult> DeleteCity([FromUri] int[] ids)
		//      {
		//	foreach (var id in ids)
		//	{
		//		City city = await db.Cities.FindAsync(id);
		//		if (city == null)
		//		{
		//			return NotFound();
		//		}
		//		db.Cities.Remove(city);
		//	}
		//          await db.SaveChangesAsync();

		//	return StatusCode(HttpStatusCode.NoContent);
		//}

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

	public class ITransaction<T>
	{
		public int id;
		public T newValue;
		public string type;
	}

	public static class CORSConfig

	{

		public const string allowedOrigins = "http://localhost:4200";

		public const string allowedHeaders = "*";

		public const string allowedMethods = "*";

	}
}