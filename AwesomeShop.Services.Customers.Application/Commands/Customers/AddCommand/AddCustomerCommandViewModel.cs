namespace AwesomeShop.Services.Customers.Application.Commands.Customers.AddCommand;

public class AddCustomerCommandViewModel
{
    public AddCustomerCommandViewModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}