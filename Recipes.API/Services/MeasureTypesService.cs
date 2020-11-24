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
        throw new ArgumentNullException(nameof(id));
      }

      var qttyType = await _unitOkWork.MeasureTypesRepository.GetByIDAsync(id);
      
      _unitOkWork.Dispose();

      return qttyType;
    }

    public async Task<IList<MeasureType>> GetListAsync(string filterBy, string filterContent, string orderBy)
    {
      string includeProperties = null;

      Expression<Func<MeasureType, bool>> filterFunc = filterBy switch
      {
        "Id" => f => f.Id.ToString().Equals(filterContent),
        "Name" => f => f.Name.Contains(filterContent),
        _ => null
      };

      Func<IQueryable<MeasureType>, IOrderedQueryable<MeasureType>> orderByFunc = orderBy switch
      {
        "Id" => q => q.OrderBy(s => s.Id),
        "Name" => q => q.OrderBy(s => s.Name),
        _ => null
      };

      var MeasureTypes = await _unitOkWork.MeasureTypesRepository.GetAllAsync(filterFunc, orderByFunc, includeProperties);

      _unitOkWork.Dispose();

      return MeasureTypes.ToList();
    }

    public async Task<MeasureTypeDTO> InsertAsync(CreateMeasureTypeDTO createDTO)
    {
      try
      {
        var entity = _mapper.Map<MeasureType>(createDTO);
        await _unitOkWork.MeasureTypesRepository.InsertAsync(entity);
        await _unitOkWork.SaveAsync();
        var entityDTO = _mapper.Map<MeasureTypeDTO>(entity);
        
        return entityDTO;
      }
      catch(Exception ex) 
      {
        return null;
      }
    }

    public async Task<bool> DeleteAsync(object id)
    {
      return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(MeasureTypeDTO entityToDelete)
    {
      return await Task.FromResult(true);
    }
  }
}