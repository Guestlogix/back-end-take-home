namespace GuestLogixRouteAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteAirports",
                c => new
                    {
                        routeId = c.Int(nullable: false, identity: true),
                        destAirport_Id = c.Int(),
                        originAirport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.routeId)
                .ForeignKey("dbo.Airports", t => t.destAirport_Id)
                .ForeignKey("dbo.Airports", t => t.originAirport_Id)
                .Index(t => t.destAirport_Id)
                .Index(t => t.originAirport_Id);
            
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AirlineCode = c.String(),
                        AirportCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteAirports", "originAirport_Id", "dbo.Airports");
            DropForeignKey("dbo.RouteAirports", "destAirport_Id", "dbo.Airports");
            DropIndex("dbo.RouteAirports", new[] { "originAirport_Id" });
            DropIndex("dbo.RouteAirports", new[] { "destAirport_Id" });
            DropTable("dbo.Airports");
            DropTable("dbo.RouteAirports");
        }
    }
}
