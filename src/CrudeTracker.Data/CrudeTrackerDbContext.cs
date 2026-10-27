using CrudeTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace CrudeTracker.Data;

public class CrudeTrackerDbContext(DbContextOptions<CrudeTrackerDbContext> options)
    : DbContext(options)
{
    public DbSet<Series> Series => Set<Series>(); //set,series is like list[int]. we are making a set of series
    public DbSet<Observation> Observations => Set<Observation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Series>(e =>
        {
            e.ToTable("series");
            e.HasKey(s => s.Code);
        });

        modelBuilder.Entity<Observation>(e =>
        {
            e.ToTable("observations");
            e.HasKey(o => new { o.SeriesCode, o.Period });
            e.HasOne<Series>()
                .WithMany()
                .HasForeignKey(o => o.SeriesCode)
                .OnDelete(DeleteBehavior.Restrict);
                //“Each observation belongs to one Series, a series can have many observations, and the link is SeriesCode.” The <Series> and the empty () mean you’re declaring the link only. You’re not adding a .Series property to your classes
        });
    }
}