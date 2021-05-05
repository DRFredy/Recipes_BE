using AutoMapper;
using Recipes.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Domain.DTOs
{
  /// <summary>
  /// Ingredient DTO
  /// </summary>
  public class IngredientDTO
  {
    /// <summary>
    /// Ingredient Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Ingredient Name
    /// </summary>
    public string Name { get; set; }
  }
  public class CreateIngredientDTO
  {
    /// <summary>
    /// Ingredient Name (required)
    /// </summary>
    [Required] public string Name { get; set; }
  }

  public class CreateIngredientDTO_To_Ingredient__Converter : ITypeConverter<CreateIngredientDTO, Ingredient>
  {
    public Ingredient Convert(CreateIngredientDTO source, Ingredient destination, ResolutionContext context)
    {
      return new Ingredient
      {
        Name = source.Name
      };
    }
  }

  public class IngredientDTO_To_Ingredient__Converter : ITypeConverter<IngredientDTO, Ingredient>
  {
    public Ingredient Convert(IngredientDTO source, Ingredient destination, ResolutionContext context)
    {
      return new Ingredient
      {
        Id = source.Id,
        Name = source.Name
      };
    }
  }

  public class Ingredient_To_IngredientDTO__Converter : ITypeConverter<Ingredient, IngredientDTO>
  {
    public IngredientDTO Convert(Ingredient source, IngredientDTO destination, ResolutionContext context)
    {
      return new IngredientDTO
      {
        Id = source.Id,
        Name = source.Name
      };
    }
  }
}

