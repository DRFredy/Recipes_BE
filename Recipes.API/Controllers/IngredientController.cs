using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.Domain;
using Recipes.API.Services.Interfaces;
using Recipes.Domain.DTOs;
using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Recipes.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientController : ControllerBase
  {
    private readonly IIngredientsService _ingredientsService;

    public IngredientController(IIngredientsService ingredientsService)
    {
      _ingredientsService = ingredientsService;
    }

    /// <summary>
    /// Gets the specified Ingredient
    /// </summary>
    /// <param name="id">The Ingredient identifier.</param>
    /// <returns>An ingredient object.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    public async Task<IActionResult> GetByID([Required][FromRoute] int id)
    {
      IngredientDTO ingredientDTO = await _ingredientsService.GetByIDAsync(id, false);


      if (ingredientDTO == null)
      {
        return NotFound(new Response<Exception>(null, 200, "Not found"));
      }
      else
      {
        return Ok(new Response<IngredientDTO>(ingredientDTO, 200, ""));
      }
    }

    /// <summary>
    /// Gets all the specified ingredients in the system.
    /// </summary>
    /// <param name="filterBy">Field name to filter by.</param>
    /// <param name="filterContent">Content for filtering.</param>
    /// <param name="orderBy">Field name to order by.</param>
    /// <param name="desc">Boolean indicating if the ordering must be descendent.</param>
    /// <returns>An ingredients list.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll(string filterBy, string filterContent, string orderBy, bool desc = false)
    {
      IList<IngredientDTO> ingredients = await _ingredientsService.GetListAsync(filterBy, filterContent, orderBy, desc);

      return Ok(new Response<IList<IngredientDTO>>(ingredients, 200, ""));
    }

    /// <summary>
    /// Inserts an ingredient
    /// </summary>
    /// <param name="createIngredientDTO">The object containing the data to insert.</param>
    /// <returns>A 201 response code if the operation was sucessful, otherwise it responds with an error </returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Insert([FromBody] CreateIngredientDTO createIngredientDTO)
    {
      try
      {
        IngredientDTO ingredientDTO = await _ingredientsService.InsertAsync(createIngredientDTO);

        if(ingredientDTO != null)
        {
          HttpRequest request = Url.ActionContext.HttpContext.Request;
          string url = new Uri(new Uri(request.Scheme + "://" + request.Host.Value), Url.Content("insert")).ToString();

          return Created(url, new Response<IngredientDTO>(ingredientDTO, 201, "created"));
        }
        else
        {
          return Ok(new Response<string>("The ingredient was not created", 200, "insert failed"));
        }
      }
      catch(InvalidDataException)
      {
        return Conflict(new Response<string>("An ingredient with the same name already exists", 409, "insert failed"));
      }
    }

    /// <summary>
    /// Updates the entity with the provided contents
    /// </summary>
    /// <param name="ingredientDTO">The object containing the data to update.</param>
    /// <returns>A Response containing the updated MeasureType, or containing a string with an error message.</returns>
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update([FromBody] IngredientDTO ingredientDTO)
    {
      try
      {
        bool updated = await _ingredientsService.UpdateAsync(ingredientDTO);

        if(updated)
        {
          return Ok(new Response<IngredientDTO>(ingredientDTO, 200, "updated"));
        }
        else
        {
          return Ok(new Response<string>("The ingredient was not updated", 200, "update failed"));
        }
      }
      catch (InvalidDataException)
      {
        return BadRequest(new Response<string>("The specified ingredient was not found", 200, "update failed"));
      }
    }

    /// <summary>
    /// Deletes the specified ingredient
    /// </summary>
    /// <param name="ingredientDTO">The object containing the data to delete.</param>
    /// <returns>A Response containing an empty string, or containing a string with an error message.</returns>
    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete([FromBody] IngredientDTO ingredientDTO)
    {
      bool deleted = await _ingredientsService.DeleteAsync(ingredientDTO.Id);

      if (deleted)
      {
        return Ok(new Response<string>("The ingredient was deleted", 200, "deleted"));
      }
      else
      {
        return Ok(new Response<string>("The ingredient was not deleted", 200, "delete failed"));
      }
    }
  }
}