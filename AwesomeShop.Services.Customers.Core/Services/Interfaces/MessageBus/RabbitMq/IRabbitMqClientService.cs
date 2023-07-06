namespace AwesomeShop.Services.Customers.Core.Services.Interfaces.MessageBus.RabbitMq;

public interface IRabbitMqClientService
{
    void Publish(object message, string routingKey, string exchange);
}