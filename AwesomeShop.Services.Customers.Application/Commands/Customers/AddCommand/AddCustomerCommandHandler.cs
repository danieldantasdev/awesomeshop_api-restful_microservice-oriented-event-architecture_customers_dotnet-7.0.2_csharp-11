using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.Events.Interfaces;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces.Customers;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands.Customers.AddCommand;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommandInputModel, AddCustomerCommandViewModel>
{
    private readonly ICustomerRepository _repository;
    private readonly IEventProcessor _eventProcessor;

    public AddCustomerCommandHandler(ICustomerRepository repository, IEventProcessor eventProcessor)
    {
        _repository = repository;
        _eventProcessor = eventProcessor;
    }

    public async Task<AddCustomerCommandViewModel> Handle(AddCustomerCommandInputModel request,
        CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.FullName, request.BirthDate, request.Email);

        await _repository.AddAsync(customer);

        _eventProcessor.Process(customer.Events);

        return new AddCustomerCommandViewModel(customer.Id);
    }
}