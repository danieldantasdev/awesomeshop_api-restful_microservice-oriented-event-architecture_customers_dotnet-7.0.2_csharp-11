using AwesomeShop.Services.Customers.Core.Events.Interfaces;

namespace AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Events.Integrations.Customers;

public class CustomerCreatedEventIntegration : IEvent
{
    public CustomerCreatedEventIntegration(Guid id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }

    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
}