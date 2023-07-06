using MediatR;

namespace AwesomeShop.Services.Customers.Application.Commands.Customers.AddCommand;

public class AddCustomerCommandInputModel : IRequest<AddCustomerCommandViewModel>
{
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
}