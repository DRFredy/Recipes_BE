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
      IQueryable<TEntity> query = dbSet
                                  .AsNoTracking();

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
      return await dbSet
                    .FindAsync(id);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
      await Task.Run(() => dbSet.Add(entity));
    }

    public virtual async Task<bool> DeleteAsync(object id)
    {
        TEntity entity = dbSet.Find(id);

        if(entity == null)
        {
          return false;
        }

        return await DeleteAsync(entity);
    }

    public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
    {
      return await Task.Run(() => {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
          dbSet.Attach(entityToDelete);
        }

        var chgTrk = dbSet.Remove(entityToDelete);

        if(chgTrk.State == EntityState.Deleted)
        {
          return true;
        }
        else
        {
          return false;
        }
      });
    }

    public virtual void Update(TEntity entity)
    {
      dbSet.Attach(entity);
      context.Entry(entity).State = EntityState.Modified;
    }
  }
}