using MediatR;

namespace AwesomeShop.Services.Customers.Application.Queries.Customers.GetByIdCommand;

public class GetCustomerByIdQueryInputModel : IRequest<GetCustomerByIdQueryViewModel>
{
    public GetCustomerByIdQueryInputModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}