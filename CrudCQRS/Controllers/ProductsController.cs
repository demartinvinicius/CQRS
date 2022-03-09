using CrudCQRS.CQRS.Commands;
using CrudCQRS.CQRS.Queries;
using CrudCQRS.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CrudCQRS.Controllers
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
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request, CancellationToken cancellation)
        {
            return Ok(await mediator.Send(request, cancellation));
        }

        public IMediator GetMediator()
        {
            return mediator;
        }

        [HttpGet]
        public Task<List<Product>> GetAll(IMediator mediator, CancellationToken cancellation) 
            => mediator.Send(new GetAllProductQuery(), cancellation);

        [HttpGet("{Id}")]
        public Task<Product> GetById([FromRoute] GetProductByIdQuery query, CancellationToken cancellation)
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
