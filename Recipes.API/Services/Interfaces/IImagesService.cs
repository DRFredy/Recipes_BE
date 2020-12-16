using System.Threading.Tasks;

namespace Recipes.API.Services.Interfaces
{
  /// <summary>
  /// Images Service
  /// </summary>
  public interface IImagesService
  {
    /// <summary>
    /// Gets an image by its file name
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <returns>The base64 string of the image.</returns>
    Task<string> GetByFileNameAsync(string fileName);

    /// <summary>
    /// Saves an image and gives it a unique name
    /// </summary>
    /// <param name="base64String">The base64 string containing the image data.</param>
    /// <returns>The unique name given to the new image.</returns>
    Task<string> SaveAsync(string base64String);

    /// <summary>
    /// Deletes an image
    /// </summary>
    /// <param name="imgFileName">The name of the image to be deleted.</param>
    /// <returns>A boolean indicating whether the operation was successful or not.</returns>
    Task<bool> DeleteAsync(string imgFileName);
  }
}