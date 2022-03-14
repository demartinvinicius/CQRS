using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.Core;
using Nudes.Paginator.Core;
using Mapster;


namespace CrudCQRS.Features.Product.Queries.All;


public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, ResultOf<PageResult<ProductDTO>>>
{
    private ProductContext context;
    public GetAllProductQueryHandler(ProductContext context)
    {
        this.context = context;
    }
    public async Task<ResultOf<PageResult<ProductDTO>>> Handle(GetAllProductQueryRequest query, CancellationToken cancellationToken)
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

        var total = await products.CountAsync(cancellationToken);

        var items = await products.PaginateBy(query, d => d.Name).ProjectToType<ProductDTO>().ToListAsync(cancellationToken);

        return new PageResult<ProductDTO>(query, total, items);
        
    }


}
