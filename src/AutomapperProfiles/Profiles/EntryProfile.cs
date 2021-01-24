using AutoMapper;
using Data.Entities;
using Models.DTO.Input;
using Models.DTO.Output;

namespace AutomapperProfiles.Profiles
{
    public class EntryProfile: Profile
    {
        public EntryProfile()
        {
            CreateMap<EntryInputDto, Entry>();
            CreateMap<Entry, EntryOutputDto>();
        }
    }
}