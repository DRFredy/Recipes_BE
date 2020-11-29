using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.DAL
{
#pragma warning disable 1591
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<MeasureType> MeasureTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder mBuilder)
    {
      mBuilder.Entity<MeasureType>(t =>
        t.Property(p => p.Id).ValueGeneratedOnAdd());
      mBuilder.Entity<MeasureType>(t =>
        t.Property(p => p.Name).HasColumnType("nvarchar(100)"));
    }
  }

#pragma warning restore 1591
}