using AutoMapper;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Common.Mappings
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<Note, NoteViewModel>().ReverseMap();
        } 
    }
}
