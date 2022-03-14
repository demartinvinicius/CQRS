using CrudCQRS.Features.Product.Bogus;
using CrudCQRS.Features.Product.Create;
using CrudCQRS.Features.Product.Delete;
using CrudCQRS.Features.Product.Queries.All;
using CrudCQRS.Features.Product.Queries.ById;
using CrudCQRS.Features.Product.Update;
using Microsoft.AspNetCore.Mvc;

namespace CrudCQRS.Features.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductsController(IMediator mediator)
            => this.mediator = mediator;
        

        [HttpPost]
        public Task<ResultOf<int>> Create([FromBody] CreateProductRequest request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);

        [HttpGet]
        public Task<ResultOf<PageResult<ProductDTO>>> GetAll([FromQuery] GetAllProductQueryRequest query,CancellationToken cancellation)
            => mediator.Send(query, cancellation);

        [HttpGet("{Id}")]
        public Task<ResultOf<ProductDTO>> GetById([FromRoute] GetProductByIdRequest query, CancellationToken cancellation)
            => mediator.Send(query, cancellation);

        [HttpPut]
        public Task<Result> Update([FromBody] UpdateProductRequest request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);


        [HttpDelete("{Id}")]
        public Task<Result> Delete([FromRoute] DeleteProductByIdRequest request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);


        [Route("/bogus")]
        [HttpGet]
        public Task<ResultOf<List<ProductDTO>>> Generate([FromQuery] CreateBogusRequest request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);

    }
}
