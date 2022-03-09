using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudCQRS.CQRS.Queries
{
    public class GetAllProductQuery : IRequest<List<Product>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Product>>
        {
            private ProductContext context;
            public GetAllProductQueryHandler(ProductContext context)
            {
                this.context = context;
            }
            public Task<List<Product>> Handle(GetAllProductQuery query,CancellationToken cancellationToken)
            {
                return context.Products.ToListAsync(cancellationToken);
            }
        }
    }
}
