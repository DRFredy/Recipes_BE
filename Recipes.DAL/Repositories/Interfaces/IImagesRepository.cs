using Recipes.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories.Interfaces
{
  /// <summary>
  /// Images Repository
  /// </summary>
  public interface IImagesRepository
  {
    Task<string> GetByFileNameAsync(string fileName);

    Task<string> SaveAsync(string base64String);

    Task<bool> DeleteAsync(string imgFileName);
  }
}