using AutoMapper;
using Microsoft.Extensions.Configuration;
using Recipes.DAL.Repositories;
using Recipes.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Recipes.DAL
{
  public class UnitOfWork : IDisposable
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly string _webRootPath;

    private IMeasureTypesRepository _measureTypesRepository;
    private IIngredientsRepository _ingredientsRepository;

    public UnitOfWork(AppDbContext context, string webRootPath, IConfiguration config, IMapper mapper)
    {
      _webRootPath = webRootPath;
      _config = config;
      _mapper = mapper;
      _context = context;
    }

    public IMeasureTypesRepository MeasureTypesRepository
    {
      get
      {
        if (_measureTypesRepository == null)
        {
          _measureTypesRepository = new MeasureTypesRepository(_context);
        }

        return _measureTypesRepository;
      }
    }

    public IIngredientsRepository IngredientsRepository
    {
      get
      {
        if (_ingredientsRepository == null)
        {
          _ingredientsRepository = new IngredientsRepository(_context);
        }

        return _ingredientsRepository;
      }
    }

    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _context.Dispose();
        }
      }

      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}