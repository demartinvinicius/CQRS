using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Queries.All;


public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, ResultOf<List<ProductDTO>>>
{
    private ProductContext context;
    public GetAllProductQueryHandler(ProductContext context)
    {
        this.context = context;
    }
    public async Task<ResultOf<List<ProductDTO>>> Handle(GetAllProductQueryRequest query, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .Where(d =>  d.Price >= (query.MinimumPrice ?? 0)  
                   && (query.MaximumPrice == null ? true : d.Price <= query.MaximumPrice))
            .Select(d => new ProductDTO
        {
            Id = d.Id,
            Name = d.Name,
            Price = d.Price,
            }).ToListAsync(cancellationToken: cancellationToken);

        return products;
    }
}
