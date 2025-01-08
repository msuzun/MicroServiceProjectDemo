using MediatR;
using MicroServiceProject.CustomerService.Repositories.Abstract;

namespace MicroServiceProject.CustomerService.App.Features.Queries.Customer.Handler
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Models.Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCustomersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Models.Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }
    }
}
