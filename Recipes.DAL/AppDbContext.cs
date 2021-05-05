using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Entities;

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
    public DbSet<Ingredient> Ingredients { get; set; }

    protected override void OnModelCreating(ModelBuilder mBuilder)
    {
      // MeasureType
      mBuilder.Entity<MeasureType>(t =>
        t.Property(p => p.Id).ValueGeneratedOnAdd());
      mBuilder.Entity<MeasureType>(t =>
        t.Property(p => p.Name).HasColumnType("nvarchar(100)"));
      // Ingredient
      mBuilder.Entity<Ingredient>(t =>
        t.Property(p => p.Id).ValueGeneratedOnAdd());
      mBuilder.Entity<Ingredient>(t =>
        t.Property(p => p.Name).HasColumnType("nvarchar(100)"));
    }
  }

#pragma warning restore 1591
}