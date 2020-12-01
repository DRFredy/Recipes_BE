using AutoMapper;
using Microsoft.Extensions.Configuration;
using Recipes.API.Services.Interfaces;
using Recipes.DAL;
using Recipes.Models.DTOs;
using Recipes.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.API.Services
{
  public class IngredientsService : IIngredientService
  {
    private readonly AppDbContext _context;
    private readonly UnitOfWork _unitOkWork;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly string _webRootPath;

    public IngredientsService(AppDbContext context, IConfiguration config, IMapper mapper)
    {
      _mapper = mapper;
      _config = config;
      _context = context;
      _unitOkWork = new UnitOfWork(_context, _webRootPath, _config, _mapper);
      _webRootPath = Directory.GetCurrentDirectory();
    }

    public async Task<Ingredient> GetByIDAsync(int id)
    {
      if(id == int.MinValue)
      {
        return null;
      }

      Ingredient qttyType = await _unitOkWork.IngredientsRepository.GetByIDAsync(id);
      
      _unitOkWork.Dispose();

      return qttyType;
    }

    public async Task<IList<Ingredient>> GetListAsync(string filterBy, string filterContent, string orderBy, bool desc = false)
    {
      string includeProperties = null;
      Expression<Func<Ingredient, bool>> filterFunc = null;
      Func<IQueryable<Ingredient>, IOrderedQueryable<Ingredient>> orderByFunc = null;
      IEnumerable<Ingredient> measureTypes = null;

      try 
      {
        if(!string.IsNullOrWhiteSpace(filterBy) && !string.IsNullOrWhiteSpace(filterContent))
        {
          filterFunc = filterBy.ToUpper() switch
          {
            "ID" => f => f.Id.ToString().Equals(filterContent),
            "NAME" => f => f.Name.Contains(filterContent),
            _ => null
          };
        }

        if(!string.IsNullOrWhiteSpace(orderBy))
        {
          if(desc)
          {
            orderByFunc = orderBy.ToUpper() switch
            {
              "ID" => q => q.OrderByDescending(s => s.Id),
              "NAME" => q => q.OrderByDescending(s => s.Name),
              _ => null
            };
          }
          else
          {
            orderByFunc = orderBy.ToUpper() switch
            {
              "ID" => q => q.OrderBy(s => s.Id),
              "NAME" => q => q.OrderBy(s => s.Name),
              _ => null
            };
          }
        }

        measureTypes = await _unitOkWork.IngredientsRepository.GetAllAsync(filterFunc, orderByFunc, includeProperties);

        _unitOkWork.Dispose();

        return measureTypes.ToList();
      }
      catch
      {
        return new List<Ingredient>();
      }
    }

    public async Task<IngredientDTO> InsertAsync(CreateIngredientDTO createDTO)
    {
      IngredientDTO entityDTO = null;

      //TODO: validate that the ingredient does not exist....
      try
      {
        var entity = _mapper.Map<Ingredient>(createDTO);
        await _unitOkWork.IngredientsRepository.InsertAsync(entity);
        await _unitOkWork.SaveAsync();
        entityDTO = _mapper.Map<IngredientDTO>(entity);
      }
      catch 
      { }

      return entityDTO;
    }

    public async Task<bool> UpdateAsync(IngredientDTO updateDTO)
    {
      bool updated = false;

      try
      {
        var entity = _mapper.Map<Ingredient>(updateDTO);
        await _unitOkWork.IngredientsRepository.UpdateAsync(entity);
        await _unitOkWork.SaveAsync();
        updated = true;
      }
      catch 
      { }

      return updated;
    }

    public async Task<bool> DeleteAsync(object id)
    {
      bool deleted =  await _unitOkWork.IngredientsRepository.DeleteAsync(id);

      if(deleted)
      {
        await _unitOkWork.SaveAsync();
      }

      return deleted;
    }

    public async Task<bool> DeleteAsync(IngredientDTO entity)
    {
      bool deleted = await _unitOkWork.MeasureTypesRepository.DeleteAsync(entity);

      if(deleted)
      {
        await _unitOkWork.SaveAsync();
      }

      return deleted;
    }
  }
}