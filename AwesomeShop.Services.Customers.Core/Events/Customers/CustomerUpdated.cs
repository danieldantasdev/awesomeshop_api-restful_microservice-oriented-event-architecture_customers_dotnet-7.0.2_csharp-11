using AwesomeShop.Services.Customers.Core.Events.Interfaces;
using AwesomeShop.Services.Customers.Core.ValueObjects;

namespace AwesomeShop.Services.Customers.Core.Events.Customers;

public class CustomerUpdated : IDomainEvent
{
    public CustomerUpdated(Guid id, string phoneNumber, AddressValueObject addressValueObject)
    {
        Id = id;
        AddressValueObject = addressValueObject;
    }

    public Guid Id { get; private set; }
    public AddressValueObject AddressValueObject { get; private set; }
}