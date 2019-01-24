namespace CityService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using CityService.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<CityService.Models.CityServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CityService.Models.CityServiceContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.

			context.Cities.AddOrUpdate(x => x.CityID,
				new City()
				{
					CityID = 1,
					CityName = "Madrid",
					Population = 3166000,
					TrainStation = true,
					HolidayDate = new DateTime(2019, 04, 02),
					Description = "Madrid is the capital of Spain"
				},
				new City()
				{
					CityID = 2,
					CityName = "Collado Villalba",
					Population = 62684,
					TrainStation = true,
					HolidayDate = new DateTime(2019, 07, 12),
					Description = "Collado Villalba has a hot summer Mediterranean climate"
				},
				new City()
				{
					CityID = 3,
					CityName = "Galapagar",
					Population = 32404,
					TrainStation = false,
					HolidayDate = new DateTime(2019, 06, 20),
					Description = "The Spanish writer Jacinto Benavente, who won the 1922 Nobel Prize for Literature, is buried in Galapagar."
				},
				new City()
				{
					CityID = 4,
					CityName = "El Escorial",
					Population = 15244,
					TrainStation = true,
					HolidayDate = new DateTime(2019, 10, 20),
					Description = "The famous Royal Seat of San Lorenzo de El Escorial is located here."
				}
			);
		}
    }
}
