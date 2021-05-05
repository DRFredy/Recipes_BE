using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Interfaces.Repository;
using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
  public class IngredientsRepository : GenericRepository<Ingredient>, IIngredientsRepository
  {
    private readonly AppDbContext _context;

    public IngredientsRepository(AppDbContext context)
      : base(context)
    {
      _context = context;
    }

    public async override Task<IEnumerable<Ingredient>> GetAllAsync(
      Expression<Func<Ingredient, bool>> filter = null,
      Func<IQueryable<Ingredient>, IOrderedQueryable<Ingredient>> orderBy = null,
      string includeProperties = "")
    {
      return await base.GetAllAsync(filter, orderBy, includeProperties);
    }

    public async Task<Ingredient> GetByIDAsync(object id, bool asNoTracking)
    {
      Ingredient entity = null;

      if(asNoTracking)
      {
        entity = await _context.Ingredients
                        .AsNoTracking()
                        .FirstOrDefaultAsync(ing => ing.Id == (int)id);
      }
      else
      {
        entity = await base.GetByIDAsync(id);
      }

      return entity;
    }

    public async Task<Ingredient> GetByNameAsync(string name)
    {
      return await _context.Ingredients
                                .FirstOrDefaultAsync(ing => ing.Name.Equals(name.Trim()));
    }

    public async override Task InsertAsync(Ingredient entity)
    {
      await base.InsertAsync(entity);
    }

    public async override Task<bool> DeleteAsync(object id)
    {
      return await base.DeleteAsync(id);
    }

    public async override Task<bool> DeleteAsync(Ingredient entity)
    {
      return await base.DeleteAsync(entity);
    }

    public async Task<bool> UpdateAsync(Ingredient entity)
    {
      return await Task.Run(() => {
        try
        {
          base.Update(entity);
        }
        catch
        {
          return false;
        }

        return true;
      });
    }
  }
}