using AwesomeShop.Services.Customers.Core.Events;
using AwesomeShop.Services.Customers.Core.Events.Addresses;
using AwesomeShop.Services.Customers.Core.Events.Customers;
using AwesomeShop.Services.Customers.Core.ValueObjects;

namespace AwesomeShop.Services.Customers.Core.Entities;

public class Customer : AggregateRoot
{
    public Customer(Guid id, string fullName, DateTime birthDate, string email)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
        Email = email;
    }

    public string FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string PhoneNumber { get; private set; }
    public AddressValueObject AddressValueObject { get; private set; }
    public string Email { get; private set; }

    public static Customer Create(string fullName, DateTime birthDate, string email)
    {
        var customer = new Customer(Guid.NewGuid(), fullName, birthDate, email);

        customer.AddEvent(new CustomerCreated(customer.Id, customer.FullName, customer.Email));

        return customer;
    }

    private void SetAddress(AddressValueObject addressValueObject)
    {
        AddressValueObject = addressValueObject;

        AddEvent(new AddressUpdated(Id, AddressValueObject.GetFullAddress()));
    }

    public void Update(string phoneNumber, AddressValueObject addressValueObject)
    {
        PhoneNumber = phoneNumber;
        SetAddress(addressValueObject);
        AddEvent(new CustomerUpdated(Id, PhoneNumber, AddressValueObject));
    }
}