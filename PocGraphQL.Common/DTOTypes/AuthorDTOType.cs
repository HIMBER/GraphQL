using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.DTOs;
using PocGraphQL.Common.Model;
using PocGraphQL.Common.ModelTypes;

namespace PocGraphQL.Common.Types;

public class AuthorDTOType : ObjectType<AuthorDTO>
{
    protected override void Configure(IObjectTypeDescriptor<AuthorDTO> descriptor)
    {
        descriptor.Field(f => f.AuthorAddress) // Ajoute le champ address
            .Resolve(context =>
            {
                var key = context.Parent<AuthorDTO>().Id;
                var cancellationToken = context.RequestAborted;

                return context.DataLoader<AddressByAuthorIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AddressType>>();
    }
    
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, AddressDTO>> GetAddressByAuthorDTOIdAsync(
        IReadOnlyList<int> keys,
        ApiContext context,
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        var entities = await context.Addresses.Where(address => keys.Contains(address.AuthorId)).ToListAsync(cancellationToken);
        var mapped = mapper.Map<List<Address>,IList<AddressDTO>>(entities);
        return mapped.ToDictionary(ad => ad.Id);
    }
}