using Microsoft.Extensions.DependencyInjection;
using Recipes.DAL.Configuration;

namespace Recipes.DAL.Providers
{
  public interface IDataProvider
  {
    DataProvider Provider { get; }
    IServiceCollection RegisterDbContext(IServiceCollection services, string connectionString);
    AppDbContext CreateDbContext(string connectionString);
  }
}