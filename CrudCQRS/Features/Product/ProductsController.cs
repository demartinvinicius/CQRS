using CrudCQRS.CQRS.Commands;
using CrudCQRS.CQRS.Queries;
using CrudCQRS.DTO;
using CrudCQRS.Features.Product.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nudes.Retornator.Core;


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
        public Task<List<Models.Product>> GetAll(IMediator mediator, CancellationToken cancellation)
            => mediator.Send(new GetAllProductQuery(), cancellation);

        [HttpGet("{Id}")]
        public Task<ResultOf<ProductDTO>> GetById([FromRoute] GetProductByIdQuery query, CancellationToken cancellation)
            => mediator.Send(query, cancellation);

        [HttpPut("{id}")]
        public Task<int> Update([FromRoute] int id, [FromBody] UpdateProductCommand request, CancellationToken cancellation)
        {
            request.Id = id;
            return mediator.Send(request, cancellation);
        }

        [HttpDelete("{id}")]
        public Task<int> Delete([FromRoute] DeleteProductByIdCommand request, CancellationToken cancellation)
        {
            return mediator.Send(request, cancellation);
        }


    }
}
