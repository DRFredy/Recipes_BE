using AutoMapper;
using Recipes.Domain.Entities;

namespace Recipes.Domain.DTOs
{
  public class DomainProfile : Profile
  {
    public DomainProfile()
    {
      // MeasureType
      CreateMap<CreateMeasureTypeDTO, MeasureType>().ConvertUsing<CreateMeasureTypeDTO_To_MeasureType__Converter>();
      CreateMap<MeasureTypeDTO, MeasureType>().ConvertUsing<MeasureTypeDTO_To_MeasureType__Converter>();
      CreateMap<MeasureType, MeasureTypeDTO>().ConvertUsing<MeasureType_To_MeasureTypeDTO__Converter>();
      // Ingredient
      CreateMap<CreateIngredientDTO, Ingredient>().ConvertUsing<CreateIngredientDTO_To_Ingredient__Converter>();
      CreateMap<IngredientDTO, Ingredient>().ConvertUsing<IngredientDTO_To_Ingredient__Converter>();
      CreateMap<Ingredient, IngredientDTO>().ConvertUsing<Ingredient_To_IngredientDTO__Converter>();
    }
  }
}