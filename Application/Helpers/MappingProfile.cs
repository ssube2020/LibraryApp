using AutoMapper;
using Core.Entities;
using Application.Responses;

namespace LibraryAppAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            //CreateMap<Author, AuthorDto>();
        }
    }
}
