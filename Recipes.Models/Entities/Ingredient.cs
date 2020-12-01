using System.ComponentModel.DataAnnotations;

namespace Recipes.Models.Entities
{
  public class Ingredient
  {
    /// <summary>
    /// Ingredient Id
    /// </summary>
    [Required] public int Id { get; set; }
    /// <summary>
    /// Ingredient Name
    /// </summary>
    [Required] public string Name { get; set; }
  }
}

