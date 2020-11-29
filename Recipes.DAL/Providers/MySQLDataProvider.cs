using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipes.DAL.Configuration;

namespace Recipes.DAL.Providers
{
  public class MySQLDataProvider : IDataProvider
  {
    public DataProvider Provider { get; } = DataProvider.MySQL;

    public IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString)
    {
      services.AddDbContext<AppDbContext>(options =>
          //options.UseMySQL(connectionString));  //MySql.Data.EntityFrameworkCore
          options.UseMySql(connectionString));    //Pomelo.EntityFrameworkCore.MySql
      return services;
    }

    public AppDbContext CreateDbContext(string connectionString)
    {
      var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
      //optionsBuilder.UseMySQL(connectionString);  //MySql.Data.EntityFrameworkCore
      optionsBuilder.UseMySql(connectionString);    //Pomelo.EntityFrameworkCore.MySql

      return new AppDbContext(optionsBuilder.Options);
    }
  }
}