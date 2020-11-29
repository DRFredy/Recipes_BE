using AutoMapper;
using Microsoft.Extensions.Configuration;
using Recipes.API.Services.Interfaces;
using Recipes.DAL;
using Recipes.Models;
using Recipes.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.API.Services
{
  public class MeasureTypesService : IMeasureTypesService
  {
    private readonly AppDbContext _context;
    private readonly UnitOfWork _unitOkWork;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly string _webRootPath;

    public MeasureTypesService(AppDbContext context, IConfiguration config, IMapper mapper)
    {
      _mapper = mapper;
      _config = config;
      _context = context;
      _unitOkWork = new UnitOfWork(_context, _webRootPath, _config, _mapper);
      _webRootPath = Directory.GetCurrentDirectory();
    }

    public async Task<MeasureType> GetByIDAsync(int id)
    {
      if(id == int.MinValue)
      {
        return null;
      }

      MeasureType qttyType = await _unitOkWork.MeasureTypesRepository.GetByIDAsync(id);
      
      _unitOkWork.Dispose();

      return qttyType;
    }

    public async Task<IList<MeasureType>> GetListAsync(string filterBy, string filterContent, string orderBy, bool desc = false)
    {
      string includeProperties = null;
      Expression<Func<MeasureType, bool>> filterFunc = null;
      Func<IQueryable<MeasureType>, IOrderedQueryable<MeasureType>> orderByFunc = null;
      IEnumerable<MeasureType> measureTypes = null;

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

        measureTypes = await _unitOkWork.MeasureTypesRepository.GetAllAsync(filterFunc, orderByFunc, includeProperties);

        _unitOkWork.Dispose();

        return measureTypes.ToList();
      }
      catch
      {
        return new List<MeasureType>();
      }
    }

    public async Task<MeasureTypeDTO> InsertAsync(CreateMeasureTypeDTO createDTO)
    {
      MeasureTypeDTO entityDTO = null;

      try
      {
        var entity = _mapper.Map<MeasureType>(createDTO);
        await _unitOkWork.MeasureTypesRepository.InsertAsync(entity);
        await _unitOkWork.SaveAsync();
        entityDTO = _mapper.Map<MeasureTypeDTO>(entity);
      }
      catch 
      { }

      return entityDTO;
    }

    public async Task<bool> UpdateAsync(MeasureTypeDTO updateDTO)
    {
      bool updated = false;

      try
      {
        var entity = _mapper.Map<MeasureType>(updateDTO);
        await _unitOkWork.MeasureTypesRepository.UpdateAsync(entity);
        await _unitOkWork.SaveAsync();
        updated = true;
      }
      catch 
      { }

      return updated;
    }

    public async Task<bool> DeleteAsync(object id)
    {
      bool deleted =  await _unitOkWork.MeasureTypesRepository.DeleteAsync(id);

      if(deleted)
      {
        await _unitOkWork.SaveAsync();
      }

      return deleted;
    }

    public async Task<bool> DeleteAsync(MeasureTypeDTO entity)
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