using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipes.API.Domain;
using Recipes.API.Services.Interfaces;
using Recipes.Models;
using Recipes.Models.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
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
    /// Gets the specified MeasureType.
    /// </summary>
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
      var qttyType = await _measureTypesService.GetByIDAsync(id);


      if (qttyType == null)
      {
        return NotFound(new Response<Exception>(null, 200, "Not found"));
      }
      else
      {
        return Ok(new Response<MeasureType>(qttyType, 200, ""));
      }
    }

    /// <summary>
    /// Gets all the specified MeasureTypes in the system.
    /// </summary>
    /// <returns>A MeasureType objects list.</returns>
    /// [ProducesResponseType(typeof(IList<MeasureType>), 200)]
    [HttpGet("getall")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll(string filterBy, string filterContent, string orderBy)
    {
      return Ok(await _measureTypesService.GetListAsync(filterBy, filterContent, orderBy));
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Insert([FromBody] CreateMeasureTypeDTO createMeasureTypeDTO)
    {
      var MeasureTypeDTO = await _measureTypesService.InsertAsync(createMeasureTypeDTO);

      HttpRequest request = Url.ActionContext.HttpContext.Request;
      string url = new Uri(new Uri(request.Scheme + "://" + request.Host.Value), Url.Content("insert")).ToString();

      return Created(url, new Response<MeasureTypeDTO>(MeasureTypeDTO, 201, ""));
    }
  }
}