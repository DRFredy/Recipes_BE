using System.ComponentModel.DataAnnotations;

namespace Recipes.Models.Entities
{
  /// <summary>
  /// Ingredient entity
  /// </summary>
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

