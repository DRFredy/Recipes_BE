using Recipes.Models.DTOs;
using Recipes.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.API.Services.Interfaces
{
  /// <summary>
  /// Ingredients Service
  /// </summary>
  public interface IIngredientsService
  {
    /// <summary>
    /// Gets an ingredient by its identifier
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="asNoTracking">Indicates whether the entity must be tracked.</param>
    /// <returns>An ingredient.</returns>
    Task<IngredientDTO> GetByIDAsync(int id, bool asNoTracking);

    /// <summary>
    /// Gets a list of ingredients
    /// </summary>
    /// <param name="filterBy">Field name to apply the filtering.</param>
    /// <param name="filterContent">Text to use by filtering.</param>
    /// <param name="orderBy">Field name to apply the ordering.</param>
    /// <param name="desc">Boolean to indicate whether the ordering must be descendent.</param>
    /// <returns>A list of ingredients.</returns>
    Task<IList<IngredientDTO>> GetListAsync(string filterBy, string filterContent, string orderBy, bool desc = false);

    /// <summary>
    /// Inserts a new ingredient
    /// </summary>
    /// <param name="entity">The object containing the data to be inserted.</param>
    /// <returns>An ingredient.</returns>
    Task<IngredientDTO> InsertAsync(CreateIngredientDTO createDTO);

    /// <summary>
    /// Updates an existing ingredient
    /// </summary>
    /// <param name="updateDTO">The object containing the data to be updated.</param>
    /// <returns>An ingredient.</returns>
    Task<bool> UpdateAsync(IngredientDTO updateDTO);

    /// <summary>
    /// Removes an ingredient from the repository
    /// </summary>
    /// <param name="id">The identifier of the ingredient to be deleted.</param>
    /// <returns>A boolean indicating whether the operation was successful or not.</returns>
    Task<bool> DeleteAsync(object id);

    /// <summary>
    /// Removes an ingredient from the repository
    /// </summary>
    /// <param name="entityToDelete">The object that represents the ingredient to be deleted.</param>
    /// <returns>A boolean indicating whether the operation was successful or not.</returns>
    Task<bool> DeleteAsync(IngredientDTO entityToDelete);
  }
}