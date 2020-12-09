using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Recipes.API.Services;
using Recipes.API.Services.Interfaces;
using Recipes.DAL.Configuration;
using Recipes.DAL.Extensions;
using Recipes.Models.DTOs;
using Recipes.Models.Entities;
using Recipes.Models.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace Recipes.API
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
    {
      var builder = GetConfigurationBuilder(env);
      Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions();

      string dataProviderName = Configuration.GetDataProviderName();
      string appVer = Configuration.GetAppVersion();
      string appTitle = Configuration.GetAppTitle();
      DataProvider dataProvider = (DataProvider)Enum.Parse(typeof(DataProvider), dataProviderName);

      services.Configure<Recipes.DAL.Configuration.Data>(c =>
      {
        c.Provider = dataProvider;
      });

      services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

      services.AddEntityFramework(Configuration);

      services.AddTransient<IMeasureTypesService, MeasureTypesService>();
      services.AddTransient<IIngredientsService, IngredientsService>();

      // Mappers:
      // MeasureType
      services.AddTransient<ITypeConverter<CreateMeasureTypeDTO, MeasureType>, CreateMeasureTypeDTO_To_MeasureType__Converter>();
      services.AddTransient<ITypeConverter<MeasureTypeDTO, MeasureType>, MeasureTypeDTO_To_MeasureType__Converter>();
      services.AddTransient<ITypeConverter<MeasureType, MeasureTypeDTO>, MeasureType_To_MeasureTypeDTO__Converter>();
      // Ingredient
      services.AddTransient<ITypeConverter<CreateIngredientDTO, Ingredient>, CreateIngredientDTO_To_Ingredient__Converter>();
      services.AddTransient<ITypeConverter<IngredientDTO, Ingredient>, IngredientDTO_To_Ingredient__Converter>();
      services.AddTransient<ITypeConverter<Ingredient, IngredientDTO>, Ingredient_To_IngredientDTO__Converter>();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddCors();

      services.AddControllers();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc(appVer, new OpenApiInfo { Title = appTitle, Version = appVer });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      string swaggerFilePaths = Configuration.GetSwaggerJsonFilePaths(Configuration.GetAppVersion());
      string appTitle = Configuration.GetAppTitle();
      string appVer = Configuration.GetAppVersion();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint($"{swaggerFilePaths}/swagger.json", $"{appTitle} {appVer}"));
      }

      app.UseCors(options =>
          options
          .AllowAnyOrigin()
          .WithHeaders("authorization", "content-type", "x-api-applicationid", "access-control-allow-origin")
          .AllowAnyHeader()
          .AllowAnyMethod()
      );

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    /// <summary>
    /// BuildBuilder just returns a ConfigurationBuilder set from appsettings in a specific directory.
    /// </summary>
    /// <param name="env">a Microsoft.AspNetCore.Hosting.IWebHostEnvironment instance</param>
    /// <returns>a ConfigurationBuilder object</returns>
    public virtual IConfigurationBuilder GetConfigurationBuilder(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
    {
      string pathToConf = GetPathToConfigFile(env.ContentRootPath);  //env.ContentRootPath sometimes does not contain the path where the config file is

      return new ConfigurationBuilder()
         .SetBasePath(env.ContentRootPath)
         .AddJsonFile(Path.Combine(pathToConf, "appsettings.json"), optional: false, reloadOnChange: true)
         .AddJsonFile(Path.Combine(pathToConf, $"appsettings.{env.EnvironmentName}.json"), optional: true)
         .AddEnvironmentVariables();
    }

    /// <summary>
    /// Gets the path to the config file
    /// </summary>
    /// <param name="envPath">the environment content root path</param>
    /// <returns>A string containing the path to the configuration file</returns>
    public string GetPathToConfigFile(string envPath)
    {
      string pathToConf;

      if (File.Exists(Path.Combine(envPath, "appsettings.json")))
      {
        pathToConf = envPath;
      }
      else
      {
        pathToConf = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      }

      return pathToConf;
    }
  }
}
