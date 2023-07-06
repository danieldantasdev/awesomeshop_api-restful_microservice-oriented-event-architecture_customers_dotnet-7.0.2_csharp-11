using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.Entities.Interfaces;
using AwesomeShop.Services.Customers.Core.Events.Interfaces;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces.Customers;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces.Mongo;
using AwesomeShop.Services.Customers.Core.Services.Interfaces.MessageBus.RabbitMq;
using AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories.Implementations.Customers;
using AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories.Implementations.Mongo;
using AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories.Options;
using AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Connections;
using AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.Events;
using AwesomeShop.Services.Customers.Infrastructure.Services.Implementations.MessageBus.RabbitMq;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Customers.Api.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddMongoExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();
            var options = new MongoDbOption();

            configuration?.GetSection("MongoDb").Bind(options);

            return options;
        });

        serviceCollection.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetService<MongoDbOption>();
            return new MongoClient(options?.ConnectionString);
        });

        serviceCollection.AddTransient(sp =>
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var options = sp.GetService<MongoDbOption>();
            var mongoClient = sp.GetService<IMongoClient>();

            return mongoClient?.GetDatabase(options?.Database);
        });

        return serviceCollection;
    }

    public static IServiceCollection AddRepositoryExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMongoRepository<Customer>("customers");
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddRabbitMqExtension(this IServiceCollection serviceCollection)
    {
        var connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection("customers-service-producer");

        serviceCollection.AddSingleton(new ProducerConnection(connection));
        serviceCollection.AddSingleton<IRabbitMqClientService, RabbitMqClientService>();
        serviceCollection.AddTransient<IEventProcessor, EventProcessor>();

        return serviceCollection;
    }

    private static IServiceCollection AddMongoRepository<T>(this IServiceCollection serviceCollection,
        string collection) where T : IEntityBase
    {
        serviceCollection.AddScoped<IMongoRepository<T>>(f =>
        {
            var mongoDatabase = f.GetRequiredService<IMongoDatabase>();

            return new MongoRepository<T>(mongoDatabase, collection);
        });

        return serviceCollection;
    }
}