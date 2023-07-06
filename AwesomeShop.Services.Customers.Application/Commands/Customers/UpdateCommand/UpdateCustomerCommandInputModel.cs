using AwesomeShop.Services.Customers.Application.Dtos;
using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands.Customers.UpdateCommand;

public class UpdateCustomerCommandInputModel : IRequest<UpdateCustomerCommandViewModel>
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public AddressDto Address { get; set; }
}