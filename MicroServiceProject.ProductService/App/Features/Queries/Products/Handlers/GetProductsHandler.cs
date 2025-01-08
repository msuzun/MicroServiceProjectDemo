using AutoMapper;
using MediatR;
using MicroServiceProject.ProductService.App.DTOs;
using MicroServiceProject.ProductService.App.Features.Queries.Products;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.App.Features.Queries.Products.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<GetProductsDto>>(products);
        }
    }
}
