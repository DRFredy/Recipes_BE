namespace Recipes.Models.Entities
{
  /// <summary>
  /// ImageData entity
  /// </summary>
  public class ImageData
  {
    /// <summary>
    /// Image filename
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Base 64 image content
    /// </summary>
    public string Base64Content { get; set; }
  }
}
