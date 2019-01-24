namespace CityService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        Population = c.Int(nullable: false),
                        TrainStation = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cities");
        }
    }
}
