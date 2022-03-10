
using CrudCQRS.CQRS.Queries;
using CrudCQRS.DTO;
using CrudCQRS.Features.Product.Create;
using CrudCQRS.Features.Product.Delete;
using CrudCQRS.Features.Product.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Retornator.Core;
using CrudCQRS.Features.Product.Update;


namespace CrudCQRS.Features.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IMediator mediator;
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] CreateProductRequest request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);

        [HttpGet]
        public Task<ResultOf<List<ProductDTO>>> GetAll(CancellationToken cancellation)
            => mediator.Send(new GetAllProductQuery(), cancellation);

        [HttpGet("{Id}")]
        public Task<ResultOf<ProductDTO>> GetById([FromRoute] GetProductByIdQuery query, CancellationToken cancellation)
            => mediator.Send(query, cancellation);

        [HttpPut("{id}")]
        public Task<ResultOf<int>> Update([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellation)
        {
            request.Id = id;
            return mediator.Send(request, cancellation);
        }

        [HttpDelete("{id}")]
        public Task<ResultOf<int>> Delete([FromRoute] int id,[FromBody] DeleteProductByIdRequest request, CancellationToken cancellation)
        {
            request.Id=id;
            return mediator.Send(request, cancellation);
        }


    }
}
