using AutoMapper;
using Data.Entities;
using Models.DTO.Input;
using Models.DTO.Output;

namespace AutomapperProfiles.Profiles
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountInputDto, Account>();
            CreateMap<Account, AccountOutputDto>();
        }
    }
}