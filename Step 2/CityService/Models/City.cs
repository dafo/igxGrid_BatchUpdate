using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityService.Models
{
	public class City
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CityID { get; set; }
		public string CityName { get; set; }
		public int Population { get; set; }
		public bool TrainStation { get; set; }
		public System.DateTimeOffset HolidayDate { get; set; }
		public string Description { get; set; }
	}
}
