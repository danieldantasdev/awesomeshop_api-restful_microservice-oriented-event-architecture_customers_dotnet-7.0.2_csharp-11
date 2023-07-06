using AwesomeShop.Services.Customers.Core.ValueObjects;

namespace AwesomeShop.Services.Customers.Application.Dtos;

public class AddressDto
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public AddressValueObject ToEntity()
        => new AddressValueObject(Street, Number, City, State, ZipCode);

    public static AddressDto ToDto(AddressValueObject address)
        => new AddressDto
        {
            Street = address.Street,
            Number = address.Number,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode
        };
}