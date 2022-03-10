using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudCQRS.CQRS.Queries
{
    public class GetAllProductQuery : IRequest<ResultOf<List<ProductDTO>>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ResultOf<List<ProductDTO>>>
        {
            private ProductContext context;
            public GetAllProductQueryHandler(ProductContext context)
            {
                this.context = context;
            }
            public async Task<ResultOf<List<ProductDTO>>> Handle(GetAllProductQuery query,CancellationToken cancellationToken)
            {
                var products = await context.Products.Select(d => new ProductDTO {
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                }).ToListAsync();
                
                return products;
            }
        }
    }
}
