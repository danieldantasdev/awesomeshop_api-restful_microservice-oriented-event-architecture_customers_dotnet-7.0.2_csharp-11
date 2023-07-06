using AwesomeShop.Services.Customers.Core.Events.Interfaces;

namespace AwesomeShop.Services.Customers.Core.Events.Addresses;

public class AddressUpdated : IDomainEvent
{
    public AddressUpdated(Guid customerId, string fullAddress)
    {
        CustomerId = customerId;
        FullAddress = fullAddress;
    }

    public Guid CustomerId { get; private set; }
    public string FullAddress { get; private set; }
}