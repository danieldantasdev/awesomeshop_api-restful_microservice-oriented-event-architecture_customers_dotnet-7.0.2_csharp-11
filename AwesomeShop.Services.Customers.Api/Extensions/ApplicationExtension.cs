using AwesomeShop.Services.Customers.Application.Commands.Customers.AddCommand;
using AwesomeShop.Services.Customers.Application.Subscribers;
using AwesomeShop.Services.Customers.Application.Subscribers.Customers;

namespace AwesomeShop.Services.Customers.Api.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddMediatRExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly,
            typeof(AddCustomerCommandInputModel).Assembly);
        });
        return serviceCollection;
    }

    public static IServiceCollection AddSubscriberExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<CustomerCreatedSubscriber>();
        return serviceCollection;
    }
}