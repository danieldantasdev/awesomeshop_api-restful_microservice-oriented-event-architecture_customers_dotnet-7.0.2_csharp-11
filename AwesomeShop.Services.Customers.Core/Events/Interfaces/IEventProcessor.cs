namespace AwesomeShop.Services.Customers.Core.Events.Interfaces;

public interface IEventProcessor
{
    void Process(IEnumerable<IDomainEvent> events);

}