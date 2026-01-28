using Orbitra.Entities;
using Microsoft.EntityFrameworkCore;

namespace Orbitra.DataAccess.Context;

public class OrbitraContext : DbContext
{
    public OrbitraContext(DbContextOptions<OrbitraContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Content> Contents { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Content>()
            .HasOne(c => c.Category) 
            .WithMany() 
            .HasForeignKey(c => c.CategoryID) 
            .OnDelete(DeleteBehavior.Restrict); 
                                                

      
        modelBuilder.Entity<Content>()
            .HasOne(c => c.City)
            .WithMany()
            .HasForeignKey(c => c.CityID)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Content>()
        .Property(c => c.Latitude)
        .HasPrecision(18, 6);



        modelBuilder.Entity<Content>()
            .Property(c => c.Longitude)
            .HasPrecision(18, 6);

        modelBuilder.Entity<Content>().Property(c => c.Latitude).HasPrecision(18, 6);
        modelBuilder.Entity<Content>().Property(c => c.Longitude).HasPrecision(18, 6);
    }

}