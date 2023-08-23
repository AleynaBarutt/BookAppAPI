using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace BookApp.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //1.kısımdan gelen 2.kısma setlenir.
            CreateMap<BookDtoForUpdate, Book>();
        }
    }
}
