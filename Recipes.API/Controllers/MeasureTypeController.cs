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
  public class MeasureTypeController : ControllerBase
  {
    private readonly IMeasureTypesService _measureTypesService;

    public MeasureTypeController(IMeasureTypesService measureTypesService)
    {
      _measureTypesService = measureTypesService;
    }

    /// <summary>
    /// Gets the specified Measure Type
    /// </summary>
    /// <param name="id">The MeasureType identifier.</param>
    /// <returns>A MeasureType object.</returns>
    /// [HttpGet("get/{id}")]
    /// [ProducesResponseType(typeof(MeasureType), 200)]
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    public async Task<IActionResult> GetByID([Required][FromRoute] int id)
    {
      MeasureTypeDTO measureTypeDTO = await _measureTypesService.GetByIDAsync(id, false);


      if (measureTypeDTO == null)
      {
        return NotFound(new Response<Exception>(null, 200, "Not found"));
      }
      else
      {
        return Ok(new Response<MeasureTypeDTO>(measureTypeDTO, 200, ""));
      }
    }

    /// <summary>
    /// Gets all the specified MeasureTypes in the system
    /// </summary>
    /// <param name="filterBy">Field name to filter by.</param>
    /// <param name="filterContent">Content for filtering.</param>
    /// <param name="orderBy">Field name to order by.</param>
    /// <param name="desc">Boolean indicating if the ordering must be descendent.</param>
    /// <returns>A MeasureTypes list.</returns>
    /// [ProducesResponseType(typeof(IList<MeasureType>), 200)]
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll(string filterBy, string filterContent, string orderBy, bool desc = false)
    {
      //return Ok(await _measureTypesService.GetListAsync(filterBy, filterContent, orderBy, desc));
      IList<MeasureTypeDTO> measureTypes = await _measureTypesService.GetListAsync(filterBy, filterContent, orderBy, desc);

      return Ok(new Response<IList<MeasureTypeDTO>>(measureTypes, 200, ""));
    }

    /// <summary>
    /// Inserts a MeasureType
    /// </summary>
    /// <param name="createMeasureTypeDTO">The object containing the data to insert.</param>
    /// <returns>A 201 response code if the operation was sucessful, otherwise it responds with an error.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Insert([FromBody] CreateMeasureTypeDTO createMeasureTypeDTO)
    {
      try
      {
        MeasureTypeDTO measureTypeDTO = await _measureTypesService.InsertAsync(createMeasureTypeDTO);

        if(measureTypeDTO != null)
        {
          HttpRequest request = Url.ActionContext.HttpContext.Request;
          string url = new Uri(new Uri(request.Scheme + "://" + request.Host.Value), Url.Content("insert")).ToString();

          return Created(url, new Response<MeasureTypeDTO>(measureTypeDTO, 201, "created"));
        }
        else
        {
          return Ok(new Response<string>("The measure type was not created", 200, "insert failed"));
        }
      }
      catch (InvalidDataException)
      {
        return Conflict(new Response<string>("A measure type with the same name already exists", 409, "insert failed"));
      }
    }

    /// <summary>
    /// Updates the entity with the provided contents
    /// </summary>
    /// <param name="measureTypeDTO">The object containing the data to update.</param>
    /// <returns>A Response containing the updated MeasureType, or containing a string with an error message.</returns>
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update([FromBody] MeasureTypeDTO measureTypeDTO)
    {
      try 
      {
        bool updated = await _measureTypesService.UpdateAsync(measureTypeDTO);

        if(updated)
        {
          return Ok(new Response<MeasureTypeDTO>(measureTypeDTO, 200, "updated"));
        }
        else
        {
          return Ok(new Response<string>("The measure type was not updated", 200, "update failed"));
        }
      }
      catch (InvalidDataException)
      {
        return BadRequest(new Response<string>("The specified measure type was not found", 200, "update failed"));
      }
    }

    /// <summary>
    /// Deletes the specified measure type
    /// </summary>
    /// <param name="measureTypeDTO">The object containing the data to delete.</param>
    /// <returns>A Response containing an empty string, or containing a string with an error message.</returns>
    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete([FromBody] MeasureTypeDTO measureTypeDTO)
    {
      bool deleted = await _measureTypesService.DeleteAsync(measureTypeDTO.Id);

      if (deleted)
      {
        return Ok(new Response<string>("The measure type was deleted", 200, "deleted"));
      }
      else
      {
        return Ok(new Response<string>("The measure type was not deleted", 200, "delete failed"));
      }
    }
  }
}