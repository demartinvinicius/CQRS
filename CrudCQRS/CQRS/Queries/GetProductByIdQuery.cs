﻿using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrudCQRS.CQRS.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private ProductContext context;
            public GetProductByIdHandler(ProductContext context)
            {
                this.context = context; 
            }
            public Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                return context.Products.FirstOrDefaultAsync(a => a.Id == query.Id, cancellationToken);
            }
        }
    }
}
