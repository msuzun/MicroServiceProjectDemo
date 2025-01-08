using MediatR;

namespace MicroServiceProject.CustomerService.App.Features.Commands.Customer
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
