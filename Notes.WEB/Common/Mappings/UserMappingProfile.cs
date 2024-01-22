using AutoMapper;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Common.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
