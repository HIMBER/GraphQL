using AutoMapper;
using PocGraphQL.Common.DTOs;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.Mappers;

public class BookDTOProfile : Profile
{
    public BookDTOProfile()
    {
        CreateMap<Book, BookDTO>()
            .ForMember(dst => dst.BookTitle, opt => opt.MapFrom(src => src.Title));
    }
}