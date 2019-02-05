using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using CityService.Models;

namespace CityService.Controllers
{
	[Route("api/Cities")]
	[EnableCors(origins: CORSConfig.allowedOrigins, headers: CORSConfig.allowedHeaders, methods: CORSConfig.allowedMethods, SupportsCredentials = true)]
	public class CitiesController : ApiController
    {
		private CityServiceContext db = new CityServiceContext();
		// GET: api/Cities
		public IList<City> GetCities()
		{
			return db.Cities.ToList();
		}

		// Comment out the following if you want to use separate end-points 
		// and to process the transactions on the client-side
		
		// POST: api/Cities/UpdateCities
		public IHttpActionResult UpdateCities(ITransaction<City>[] transactions)
		{

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
						db.Cities.Add(transaction.newValue);
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
			return Ok();
		}

		// Uncomment the following if you want to use separate end points 
		// and to process the transactions on the client-side

		//[HttpPut]
		//[Route("api/Cities/Delete")]
		//[EnableCors(origins: CORSConfig.allowedOrigins, headers: CORSConfig.allowedHeaders, methods: CORSConfig.allowedMethods, SupportsCredentials = true)]
		//public IHttpActionResult DeleteCity(ITransaction<City> transaction)
		//{
		//	var city = db.Cities.Where(x => x.CityID == transaction.id).FirstOrDefault();
		//	db.Cities.Remove(city);
		//	try
		//	{
		//		db.SaveChanges();
		//	}
		//	catch (Exception e)
		//	{
		//		return BadRequest(e.Message);
		//	}
		//	return Ok();
		//}

		//[HttpPost]
		//[Route("api/Cities/Add")]
		//[EnableCors(origins: CORSConfig.allowedOrigins, headers: CORSConfig.allowedHeaders, methods: CORSConfig.allowedMethods, SupportsCredentials = true)]
		//public IHttpActionResult AddCity(ITransaction<City> transaction)
		//{
		//	db.Cities.Add(transaction.newValue);
		//	try
		//	{
		//		db.SaveChanges();
		//	}
		//	catch (Exception e)
		//	{
		//		return BadRequest(e.Message);
		//	}
		//	return Ok();
		//}

		//[HttpPut]
		//[Route("api/Cities/Update")]
		//[EnableCors(origins: CORSConfig.allowedOrigins, headers: CORSConfig.allowedHeaders, methods: CORSConfig.allowedMethods, SupportsCredentials = true)]
		//public IHttpActionResult UpdateCity(ITransaction<City> transaction)
		//{
		//	var city = db.Cities.Where(x => x.CityID == transaction.id).FirstOrDefault();
		//	db.Entry(city).CurrentValues.SetValues(transaction.newValue);
		//	try
		//	{
		//		db.SaveChanges();
		//	}
		//	catch (Exception e)
		//	{
		//		return BadRequest(e.Message);
		//	}
		//	return Ok();
		//}

		public class ITransaction<T>
		{
			public int id;
			public T newValue;
			public string type;
		}

	}

	public static class CORSConfig
	{
		public const string allowedOrigins = "http://localhost:4200";
		public const string allowedHeaders = "*";
		public const string allowedMethods = "*";
	}
}