using Microsoft.AspNetCore.Mvc;
using Recipes.API.Domain;
using Recipes.API.Services.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Recipes.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly IImagesService _imagesService;

    public ImageController(IImagesService imagesService)
    {
      _imagesService = imagesService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    public async Task<IActionResult> GetByFileName([Required][FromRoute] string fileName)
    {
      string base64img = await _imagesService.GetByFileNameAsync(fileName);

      if (string.IsNullOrWhiteSpace(base64img))
      {
        return NotFound(new Response<Exception>(null, 200, "Not found"));
      }
      else
      {
        return Ok(new Response<string>(base64img, 200, ""));
      }
    }
  }
}