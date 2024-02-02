using AutoMapper;
using PocGraphQL.Common.DTOs;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.Mappers;

public class AddressDTOProfile : Profile
{
    public AddressDTOProfile()
    {
        CreateMap<Address, AddressDTO>();
    }
}