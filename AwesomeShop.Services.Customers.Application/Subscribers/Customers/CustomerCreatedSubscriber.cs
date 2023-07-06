using System.Text;
using AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Connections;
using AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Events.Integrations.Customers;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AwesomeShop.Services.Customers.Application.Subscribers.Customers;

public class CustomerCreatedSubscriber : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _model;
    private const string QueueName = "customers-service/customers.CustomerCreatedIntegration";

    public CustomerCreatedSubscriber(IServiceProvider serviceProvider, ProducerConnection producerConnection)
    {
        _serviceProvider = serviceProvider;
        _connection = producerConnection.Connection;

        _model = _connection.CreateModel();
        _model.QueueDeclare(QueueName, false, false, false, null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_model);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonConvert.DeserializeObject<CustomerCreatedEventIntegration>(contentString);

            Console.WriteLine($"Message CustomerCreatedIntegration received with Id {message.Id}");

            _model.BasicAck(eventArgs.DeliveryTag, false);
        };

        _model.BasicConsume(QueueName, false, consumer);

        return Task.CompletedTask;
    }
}