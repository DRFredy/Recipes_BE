//using Recipes.Models.DTOs;
//using Recipes.Models.Entities;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Recipes.API.Services.Interfaces
//{
//  public interface IMeasureTypesService
//  {
//    Task<MeasureType> GetByIDAsync(int id);

//    Task<IList<MeasureType>> GetListAsync(string filterBy, string filterContent, string orderBy, bool desc = false);

//    Task<MeasureTypeDTO> InsertAsync(CreateMeasureTypeDTO entity);

//    Task<bool> UpdateAsync(MeasureTypeDTO updateDTO);

//    Task<bool> DeleteAsync(object id);

//    Task<bool> DeleteAsync(MeasureTypeDTO entityToDelete);
//  }
//}