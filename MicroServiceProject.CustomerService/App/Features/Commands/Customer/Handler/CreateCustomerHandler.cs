using MediatR;
using MicroServiceProject.CustomerService.Repositories.Abstract;

namespace MicroServiceProject.CustomerService.App.Features.Commands.Customer.Handler
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Models.Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveAsync();

            return customer.CustomerId;
        }
    }
}
