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
  public class MeasureTypesRepository : IMeasureTypesRepository
  {
    private readonly AppDbContext _context;

    public MeasureTypesRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<MeasureType>> GetAllAsync(
        Expression<Func<MeasureType, bool>> filter = null,
        Func<IQueryable<MeasureType>, IOrderedQueryable<MeasureType>> orderBy = null,
        string includeProperties = "")
    {
      IQueryable<MeasureType> query = _context.MeasureTypes;

      return await Task.Run(() => {
        if (filter != null)
        {
          query = query.Where(filter);
        }

        if(!string.IsNullOrWhiteSpace(includeProperties)) {
          foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
          {
            query = query.Include(includeProperty);
          }
        }

        if (orderBy != null)
        {
          return orderBy(query).ToList();
        }
        else
        {
          return query.ToList();
        }
      });
    }

    public async Task<MeasureType> GetByIDAsync(int id)
    {
      //Console.WriteLine("INI consulta" + DateTime.Now.ToString("HH:mm:ss.FFFFFF"));
      //var qttyType = await _context.MeasureTypes.FirstOrDefaultAsync(qt => qt.Id == id);
      //Console.WriteLine("FIN consulta " + DateTime.Now.ToString("HH:mm:ss.FFFFFF"));
      //return qttyType;
      return await _context.MeasureTypes.FirstOrDefaultAsync(qt => qt.Id == id);
    }

    public async Task InsertAsync(MeasureType entity)
    {
      await _context.MeasureTypes.AddAsync(entity);
    }

    public async Task DeleteAsync(object id)
    {
      await Task.FromResult(true);
    }

    public async Task DeleteAsync(MeasureType entityToDelete)
    {
      await Task.FromResult(true);
    }

    public async Task UpdateAsync(MeasureType entityToUpdate)
    {
      await Task.FromResult(true);
    }
  }
}