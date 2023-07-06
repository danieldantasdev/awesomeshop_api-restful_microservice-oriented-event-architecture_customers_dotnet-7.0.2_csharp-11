using System.Text;
using AwesomeShop.Services.Customers.Core.Events;
using AwesomeShop.Services.Customers.Core.Events.Customers;
using AwesomeShop.Services.Customers.Core.Events.Interfaces;
using AwesomeShop.Services.Customers.Core.Services.Interfaces.MessageBus.RabbitMq;
using AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Events.Integrations.Customers;

namespace AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Events;

public class EventProcessor : IEventProcessor
{
    private readonly IRabbitMqClientService _rabbitMqClientService;

    public EventProcessor(IRabbitMqClientService rabbitMqClientService)
    {
        _rabbitMqClientService = rabbitMqClientService;
    }

    private IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
    {
        return events.Select(Map);
    }

    private IEvent? Map(IDomainEvent @event)
        => @event switch
        {
            CustomerCreated e => new CustomerCreatedEventIntegration(e.Id, e.FullName, e.Email),
            _ => null
        };

    public void Process(IEnumerable<IDomainEvent> events)
    {
        var integrationEvents = MapAll(events);

        foreach (var @event in integrationEvents)
        {
            _rabbitMqClientService.Publish(@event, MapConvention(@event), "customer-service");
        }
    }

    private string MapConvention(IEvent @event)
    {
        return ToDashCase(@event.GetType().Name);
    }

    private string ToDashCase(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length < 2)
        {
            return text;
        }

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        Console.WriteLine($"ToDashCase: " + sb.ToString());

        return sb.ToString();
    }
}