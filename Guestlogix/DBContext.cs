using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class DBContext : IdentityDbContext{
    private IConfigurationRoot _config;
    public DBContext(DbContextOptions<DBContext> options, IConfigurationRoot config) : base(options){
        _config = config;
    }
    public DbSet<Route> Route{
        get;
        set;
    }
    public DbSet<Airport> Airport{
        get;
        set;
    }
    public DbSet<Airline> Airline{
        get;
        set;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_config["ConnectionStrings:MyConnection"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Route>().HasKey( r => new {
            r.Origin,
            r.Destination
        });
    }
}