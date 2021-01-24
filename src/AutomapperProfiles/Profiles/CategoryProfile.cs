using AutoMapper;
using Data.Entities;
using Models.DTO.Input;
using Models.DTO.Output;

namespace AutomapperProfiles.Profiles
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryInputDto, Category>();
            CreateMap<Category, CategoryOutputDto>();
        }
    }
}