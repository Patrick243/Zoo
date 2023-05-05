using Microsoft.EntityFrameworkCore;
using TransportApp.Domain;

namespace TransportApp.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Street> Streets { get; set; }

    public DbSet<BusStop> BusStops { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<Route> Routes { get; set; }

    public DbSet<House> Houses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Role>()
            .HasData(new()
            {
                Id = -2,
                Name = "user",
            }, new Role()
            {
                Id = -1,
                Name = "admin"
            });

        builder.Entity<User>()
            .HasData(new User()
            {
                Id = -1,
                Username = "Patrick",
                Password = "123456",
                RoleId = -1
            });
    }
}
