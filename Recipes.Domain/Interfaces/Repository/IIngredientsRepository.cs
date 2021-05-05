using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.Domain.Interfaces.Repository
{
  /// <summary>
  /// Ingredients Repository
  /// </summary>
  public interface IIngredientsRepository
  {
    Task<IEnumerable<Ingredient>> GetAllAsync(
        Expression<Func<Ingredient, bool>> filter = null,
        Func<IQueryable<Ingredient>, IOrderedQueryable<Ingredient>> orderBy = null,
        string includeProperties = "");

    Task<Ingredient> GetByIDAsync(object id, bool asNoTracking);

    Task<Ingredient> GetByNameAsync(string name);

    Task InsertAsync(Ingredient entity);

    Task<bool> UpdateAsync(Ingredient entity);
    
    Task<bool> DeleteAsync(object id);

    Task<bool> DeleteAsync(Ingredient entity);
  }
}