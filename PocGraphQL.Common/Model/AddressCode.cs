using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;

namespace PocGraphQL.Common.Model;

public class AddressCode: ValueObject
{
    [Required]
    public string Value { get; }

    public AddressCode(string value)
    {
        Value = value;
    }
    
    public static CSharpFunctionalExtensions.Result<AddressCode> Create(string input)
    {
        return Result.Success(new AddressCode(input));
    }
    
    public static implicit operator AddressCode(string input) => Create(input).Value;

    public static implicit operator string(AddressCode addressCode) => addressCode.Value;
    
    public override bool Equals(object obj)
    {
        return obj is AddressCode addressCode && Value.Equals(addressCode.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode(StringComparison.InvariantCultureIgnoreCase) * 397;
    }

    public override string ToString() => Value;

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}