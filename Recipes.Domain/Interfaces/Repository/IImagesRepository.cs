using System.Threading.Tasks;

namespace Recipes.Domain.Interfaces.Repository
{
  /// <summary>
  /// Images Repository
  /// </summary>
  public interface IImagesRepository
  {
    Task<string> GetByFileNameAsync(string fileName);

    Task<string> SaveAsync(string base64String);

    Task<bool> DeleteAsync(string imgFileName);
  }
}