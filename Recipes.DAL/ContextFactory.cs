using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Recipes.DAL.Configuration;
using Recipes.DAL.Interfaces;
using Recipes.DAL.Providers;
using System;
using System.Linq;

namespace Recipes.DAL
{
  public class ContextFactory : IContextFactory
    {
        private Configuration.Data DataConfiguration { get; }
        private ConnectionStrings ConnectionStrings { get; }
        private IServiceProvider _serviceProvider { get; }

        public ContextFactory(IOptions<Configuration.Data> dataOptions,
            IServiceProvider serviceProvider,
            IOptions<ConnectionStrings> connectionStringsOption)
        {
          DataConfiguration = dataOptions.Value;
          ConnectionStrings = connectionStringsOption.Value;
          _serviceProvider = serviceProvider;
        }

        public AppDbContext Create()
        {
            IDataProvider dataProvider = _serviceProvider.GetServices<IDataProvider>().FirstOrDefault(s => s.Provider == DataConfiguration.Provider);

            if (dataProvider == null)
                throw new Exception("The Data Provider entry in appsettings.json is empty or the one specified has not been found!");

            return dataProvider.CreateDbContext(ConnectionStrings.DefaultConnection);
        }
    }
}