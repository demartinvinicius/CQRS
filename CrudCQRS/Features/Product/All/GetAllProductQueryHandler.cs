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
        var products = context.Products.AsQueryable();

        if (query.MinimumPrice.HasValue)
        {
            products = products.Where(d => d.Price >= query.MinimumPrice);
        }

        if (query.MaximumPrice.HasValue)
        {
            products = products.Where(d => d.Price <= query.MaximumPrice);
        }

        List<ProductDTO> items;



        var total = await products.CountAsync(cancellationToken);

        items = await products.PaginateBy(query, d => d.Name).Select(d => new ProductDTO
        {
            Id = d.Id,
            Name = d.Name,
            Price = d.Price
        }).ToListAsync(cancellationToken);


        return items;

    }


}
