namespace GuestLogixRouteAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcommit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteAirports", "destAirport_Id", "dbo.Airports");
            DropForeignKey("dbo.RouteAirports", "originAirport_Id", "dbo.Airports");
            DropIndex("dbo.RouteAirports", new[] { "destAirport_Id" });
            DropIndex("dbo.RouteAirports", new[] { "originAirport_Id" });
            CreateTable(
                "dbo.AirportRoutes",
                c => new
                    {
                        routeId = c.Int(nullable: false, identity: true),
                        airlineCode = c.String(),
                        originAirport = c.String(),
                        destAirport = c.String(),
                    })
                .PrimaryKey(t => t.routeId);
            
            DropTable("dbo.RouteAirports");
            DropTable("dbo.Airports");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AirlineCode = c.String(),
                        AirportCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteAirports",
                c => new
                    {
                        routeId = c.Int(nullable: false, identity: true),
                        destAirport_Id = c.Int(),
                        originAirport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.routeId);
            
            DropTable("dbo.AirportRoutes");
            CreateIndex("dbo.RouteAirports", "originAirport_Id");
            CreateIndex("dbo.RouteAirports", "destAirport_Id");
            AddForeignKey("dbo.RouteAirports", "originAirport_Id", "dbo.Airports", "Id");
            AddForeignKey("dbo.RouteAirports", "destAirport_Id", "dbo.Airports", "Id");
        }
    }
}
