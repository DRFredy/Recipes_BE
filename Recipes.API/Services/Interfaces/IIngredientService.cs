using Recipes.Models.DTOs;
using Recipes.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.API.Services.Interfaces
{
  public interface IIngredientService
  {
    Task<Ingredient> GetByIDAsync<Ingredient>(int id);

    Task<IList<Ingredient>> GetListAsync(string filterBy, string filterContent, string orderBy, bool desc = false);

    Task<IngredientDTO> InsertAsync(CreateIngredientDTO entity);

    Task<bool> UpdateAsync(IngredientDTO updateDTO);

    Task<bool> DeleteAsync(object id);

    Task<bool> DeleteAsync(IngredientDTO entityToDelete);
  }
}