using AwesomeShop.Services.Customers.Core.Repositories.Interfaces;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces.Customers;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands.Customers.UpdateCommand;

public class
    UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandInputModel, UpdateCustomerCommandViewModel>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<UpdateCustomerCommandViewModel> Handle(UpdateCustomerCommandInputModel request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        customer.Update(request.PhoneNumber, request.Address.ToEntity());

        await _customerRepository.UpdateAsync(customer);

        return new UpdateCustomerCommandViewModel();
    }
}