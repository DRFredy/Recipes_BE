using Recipes.Domain.DTOs;
using Recipes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.API.Services.Interfaces
{
  public interface IMeasureTypesService
  {
    /// <summary>
    /// Gets a measure type by its identifier
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="asNoTracking">Indicates whether the entity must be tracked.</param>
    /// <returns>A measure type.</returns>
    Task<MeasureTypeDTO> GetByIDAsync(int id, bool asNoTracking);

    /// <summary>
    /// Gets a list of measure types
    /// </summary>
    /// <param name="filterBy">Field name to apply the filtering.</param>
    /// <param name="filterContent">Text to use by filtering.</param>
    /// <param name="orderBy">Field name to apply the ordering.</param>
    /// <param name="desc">Boolean to indicate whether the ordering must be descendent.</param>
    /// <returns>A list of measure types.</returns>
    Task<IList<MeasureTypeDTO>> GetListAsync(string filterBy, string filterContent, string orderBy, bool desc = false);

    /// <summary>
    /// Inserts a new measure type
    /// </summary>
    /// <param name="entity">The object containing the data to be inserted.</param>
    /// <returns>A measure type.</returns>
    Task<MeasureTypeDTO> InsertAsync(CreateMeasureTypeDTO createDTO);

    /// <summary>
    /// Updates an existing measure type
    /// </summary>
    /// <param name="updateDTO">The object containing the data to be updated.</param>
    /// <returns>A measure type.</returns>
    Task<bool> UpdateAsync(MeasureTypeDTO updateDTO);

    /// <summary>
    /// Removes a measure type from the repository
    /// </summary>
    /// <param name="id">The identifier of the measure type to be deleted.</param>
    /// <returns>A boolean indicating whether the operation was successful or not.</returns>
    Task<bool> DeleteAsync(object id);

    /// <summary>
    /// Removes a measure type from the repository
    /// </summary>
    /// <param name="entityToDelete">The object that represents the measure type to be deleted.</param>
    /// <returns>A boolean indicating whether the operation was successful or not.</returns>
    Task<bool> DeleteAsync(MeasureTypeDTO entityToDelete);
  }
}