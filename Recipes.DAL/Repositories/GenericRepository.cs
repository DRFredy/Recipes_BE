using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
  public class GenericRepository<TEntity> where TEntity : class
  {
    internal AppDbContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(AppDbContext context)
    {
      this.context = context;
      this.dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
      IQueryable<TEntity> query = dbSet;

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

    public virtual async Task<TEntity> GetByIDAsync(object id)
    {
      //return await Task.Run(() => dbSet.Find(id));
      Console.WriteLine("INI GenRepo.GetByIDAsync: " + DateTime.Now.ToString("HH:mm:ss.FFFFFF"));
      var r = await Task.Run(() => dbSet.Find(id));
      Console.WriteLine("FIN GenRepo.GetByIDAsync: " + DateTime.Now.ToString("HH:mm:ss.FFFFFF"));
      return r;
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
      await Task.Run(() => dbSet.Add(entity));
    }

    public virtual async Task DeleteAsync(object id)
    {
      await Task.Run(async () => {
        TEntity entityToDelete = dbSet.Find(id);
        await DeleteAsync(entityToDelete);
      });
    }

    public virtual async Task DeleteAsync(TEntity entityToDelete)
    {
      await Task.Run(() => {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
          dbSet.Attach(entityToDelete);
        }

        dbSet.Remove(entityToDelete);
      });
    }

    public virtual async Task UpdateAsync(TEntity entityToUpdate)
    {
      await Task.Run(() => {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
      });
    }
  }
}