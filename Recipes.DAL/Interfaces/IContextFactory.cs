namespace Recipes.DAL.Interfaces
{
  public interface IContextFactory
  {
    AppDbContext Create();
  }
}