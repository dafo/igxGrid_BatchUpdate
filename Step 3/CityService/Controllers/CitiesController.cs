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
using System.Web.Http.Description;
using CityService.Models;

namespace CityService.Controllers
{
    public class CitiesController : ApiController
    {
		private CityServiceContext db = new CityServiceContext();
		// GET: api/Cities
		public IQueryable<City> GetCities()
		{
			return db.Cities;
		}

		public IHttpActionResult UpdateCities(ITransaction<City>[] transactions)
		{

			foreach (var transaction in transactions)
			{
				if (transaction.type == "update")
				{

					// Find the city that needs to be updated
					var city = db.Cities.Where(x => x.CityID == transaction.id).FirstOrDefault();

					// Update the properties values
					db.Entry(city).CurrentValues.SetValues(transaction.newValue);

					// You can set the values property by property instead of using SetValues()

					//city.CityName = transaction.newValue.CityName;
					//city.Population = transaction.newValue.Population;
					//city.TrainStation = transaction.newValue.TrainStation;
					//city.HolidayDate = transaction.newValue.HolidayDate;
					//city.Description = transaction.newValue.Description;
				}
				else if (transaction.type == "add")
				{
					db.Cities.Add(transaction.newValue);
				}
				else if (transaction.type == "delete")
				{
					var city = db.Cities.Where(x => x.CityID == transaction.id).FirstOrDefault();
					db.Cities.Remove(city);
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
			return Ok();
		}

		public class ITransaction<T>
		{
			public int id;
			public T newValue;
			public string type;
		}

	}
}