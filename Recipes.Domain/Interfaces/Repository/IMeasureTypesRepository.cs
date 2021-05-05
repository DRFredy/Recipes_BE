using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.Domain.Interfaces.Repository
{
  /// <summary>
  /// MeasureTypes Repository
  /// </summary>
  public interface IMeasureTypesRepository
  {
    Task<IEnumerable<MeasureType>> GetAllAsync(
        Expression<Func<MeasureType, bool>> filter = null,
        Func<IQueryable<MeasureType>, IOrderedQueryable<MeasureType>> orderBy = null,
        string includeProperties = "");

    Task<MeasureType> GetByIDAsync(object id, bool asNoTracking);

    Task<MeasureType> GetByNameAsync(string name);
    Task InsertAsync(MeasureType entity);

    Task<bool> UpdateAsync(MeasureType entity);
    
    Task<bool> DeleteAsync(object id);

    Task<bool> DeleteAsync(MeasureType entity);
  }
}