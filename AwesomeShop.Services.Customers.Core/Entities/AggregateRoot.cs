using AwesomeShop.Services.Customers.Core.Entities.Interfaces;
using AwesomeShop.Services.Customers.Core.Events;
using AwesomeShop.Services.Customers.Core.Events.Interfaces;

namespace AwesomeShop.Services.Customers.Core.Entities;

public abstract class AggregateRoot : IEntityBase
{
    public Guid Id { get; protected set; }
    private List<IDomainEvent> _domainEventsList = new List<IDomainEvent>();
    public IEnumerable<IDomainEvent> Events => _domainEventsList;

    protected void AddEvent(IDomainEvent domainEvents)
    {
        if (_domainEventsList == null)
        {
            _domainEventsList = new List<IDomainEvent>();
        }

        _domainEventsList.Add(domainEvents);
    }
}