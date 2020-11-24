using System.ComponentModel.DataAnnotations;

namespace Recipes.Models
{
  /// <summary>
  /// MeasureType entity
  /// </summary>
  public class MeasureType
  {
    /// <summary>
    /// MeasureType Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// MeasureType Name
    /// </summary>
    [Required] public string Name { get; set; }
  }
}
