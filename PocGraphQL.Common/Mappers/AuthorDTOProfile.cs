using AutoMapper;
using PocGraphQL.Common.DTOs;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.Mappers;

public class AuthorDTOProfile : Profile
{
    public AuthorDTOProfile()
    {
        CreateMap<Author, AuthorDTO>()
            .ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.Name));
    }
}