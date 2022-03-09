using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudCQRS.CQRS.Queries
{
    public class GetProductByIdQuery : IRequest<ResultOf<ProductDTO>>
    {
        public int Id { get; set; }
        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ResultOf<ProductDTO>>
        {
            private ProductContext context;
            public GetProductByIdHandler(ProductContext context)
            {
                this.context = context;
            }
            public async Task<ResultOf<ProductDTO>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await context.Products
                    .Select(d => new ProductDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Price = d.Price,
                    })
                    .FirstOrDefaultAsync(a => a.Id == query.Id, cancellationToken);

                if (product == null)
                    return new NotFoundError();

                return product;
            }
        }
    }
}
