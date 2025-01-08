using MediatR;


namespace MicroServiceProject.CustomerService.App.Features.Queries.Customer
{
    public class GetCustomersQuery: IRequest<IEnumerable<Models.Customer>>
    {
    }
}
