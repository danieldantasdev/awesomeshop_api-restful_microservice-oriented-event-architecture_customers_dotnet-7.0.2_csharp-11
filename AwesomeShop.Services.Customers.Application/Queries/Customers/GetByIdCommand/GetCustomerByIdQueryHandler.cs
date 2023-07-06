using AwesomeShop.Services.Customers.Application.Dtos;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces;
using AwesomeShop.Services.Customers.Core.Repositories.Interfaces.Customers;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Queries.Customers.GetByIdCommand;

public class
    GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQueryInputModel, GetCustomerByIdQueryViewModel>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<GetCustomerByIdQueryViewModel> Handle(GetCustomerByIdQueryInputModel request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        return new GetCustomerByIdQueryViewModel(customer.Id, customer.FullName, customer.BirthDate,
            AddressDto.ToDto(customer.AddressValueObject));
    }
}