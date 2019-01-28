using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityService.Models
{
	public class City
	{
		public int CityID { get; set; }
		public string CityName { get; set; }
		public int Population { get; set; }
		public bool TrainStation { get; set; }
		public System.DateTime HolidayDate { get; set; }
		public string Description { get; set; }
	}
}
