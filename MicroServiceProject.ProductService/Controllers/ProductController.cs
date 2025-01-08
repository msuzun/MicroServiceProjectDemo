using AutoMapper;
using MediatR;
using MicroServiceProject.ProductService.App.DTOs;
using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.App.Features.Queries.Products;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceProject.ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        var createCommand = _mapper.Map<CreateProductCommand>(dto);
        var productId = await _mediator.Send(createCommand);
        return Ok(productId);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] UpdateProductDto dto)
    { 
        var updateCommand = _mapper.Map<UpdateProductCommand>(dto);
        updateCommand.ProductId = id;

        var result = await _mediator.Send(updateCommand);

        if (!result)
        {
            return NotFound("Böyle bir ürün bulunmamaktadır.");
        }

        return Ok(); // Başarılı güncelleme json döndrülmeli
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Geçersiz id değeri.");
        }
        var result = await _mediator.Send(new DeleteProductCommand(id));

        if (!result)
        {
            return NotFound("Product not found.");
        }

        return Ok(); // Başarılı silme  json döndrülmeli
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Geçersiz id değeri.");
        }
        var product = await _mediator.Send(new GetProductByIdQuery(id));

        if (product == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(product); //json döndrülmeli
    }
    [HttpGet("get-id-by-name/{name}")]
    public async Task<IActionResult> GetProductIdByName(string name)
    {
        var productId = await _mediator.Send(new GetProductIdByNameQuery(name));

        if (productId == null)
        {
            return NotFound($"Product with name '{name}' not found.");
        }

        return Ok(productId);
    }
    [HttpGet("{productId}/stock")]
    public async Task<IActionResult> GetProductStock(int productId)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(productId));
        if (product == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(product.Stock); // Ürünün mevcut stok miktarını döner  json döndrülmeli
    }
    [HttpGet("test-exception")]
    public IActionResult TestException()
    {
        throw new Exception("This is a test exception."); //json döndrülmeli
    }
}
