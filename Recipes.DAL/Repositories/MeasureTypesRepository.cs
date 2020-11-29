using Microsoft.EntityFrameworkCore;
using Recipes.DAL.Repositories.Interfaces;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
  public class MeasureTypesRepository : GenericRepository<MeasureType>, IMeasureTypesRepository
  {
    private readonly AppDbContext _context;

    public MeasureTypesRepository(AppDbContext context)
      : base(context)
    {
      _context = context;
    }

    public async override Task<IEnumerable<MeasureType>> GetAllAsync(
      Expression<Func<MeasureType, bool>> filter = null,
      Func<IQueryable<MeasureType>, IOrderedQueryable<MeasureType>> orderBy = null,
      string includeProperties = "")
    {
      return await base.GetAllAsync(filter, orderBy, includeProperties);
    }

    public async override Task<MeasureType> GetByIDAsync(object id)
    {
      return await _context.MeasureTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(et => et.Id == (int)id);
    }

    public async override Task InsertAsync(MeasureType entity)
    {
      await base.InsertAsync(entity);
    }

    public async override Task<bool> DeleteAsync(object id)
    {
      return await base.DeleteAsync(id);
    }

    public async override Task<bool> DeleteAsync(MeasureType entity)
    {
      return await base.DeleteAsync(entity);
    }

    public async Task<bool> UpdateAsync(MeasureType entity)
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

    // public async Task<IEnumerable<MeasureType>> GetAllAsync(
    //     Expression<Func<MeasureType, bool>> filter = null,
    //     Func<IQueryable<MeasureType>, IOrderedQueryable<MeasureType>> orderBy = null,
    //     string includeProperties = "")
    // {
    //   IQueryable<MeasureType> query = _context.MeasureTypes
    //                                     .AsNoTracking();

    //   return await Task.Run(() => {
    //     if (filter != null)
    //     {
    //       query = query.Where(filter);
    //     }

    //     if(!string.IsNullOrWhiteSpace(includeProperties)) {
    //       foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //       {
    //         query = query.Include(includeProperty);
    //       }
    //     }

    //     if (orderBy != null)
    //     {
    //       return orderBy(query).ToList();
    //     }
    //     else
    //     {
    //       return query.ToList();
    //     }
    //   });
    // }

    // public async Task<MeasureType> GetByIDAsync(int id)
    // {
    //   return await _context.MeasureTypes
    //                 .AsNoTracking()
    //                 .FirstOrDefaultAsync(et => et.Id == id);
    // }

    // public async override Task InsertAsync(MeasureType entity)
    // {
    //   //await _context.MeasureTypes.AddAsync(entity);
    //   await base.InsertAsync(entity);
    // }

    // public async override Task<bool> DeleteAsync(int id)
    // {
    //   return 
    // }

    // public async Task DeleteAsync(MeasureType entityToDelete)
    // {
    //   await Task.FromResult(true);
    // }

    // public async Task UpdateAsync(MeasureType entityToUpdate)
    // {
    //   await Task.FromResult(true);
    // }
  }
}