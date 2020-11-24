using AutoMapper;
using Recipes.Models;

namespace Recipes.Models.DTOs
{
  public class DomainProfile : Profile
  {
    public DomainProfile()
    {
      CreateMap<CreateMeasureTypeDTO, MeasureType>().ConvertUsing<CreateMeasureTypeDTO_To_MeasureType__Converter>();
      CreateMap<MeasureTypeDTO, MeasureType>().ConvertUsing<MeasureTypeDTO_To_MeasureType__Converter>();
      CreateMap<MeasureType, MeasureTypeDTO>().ConvertUsing<MeasureType_To_MeasureTypeDTO__Converter>();
    }
  }
}