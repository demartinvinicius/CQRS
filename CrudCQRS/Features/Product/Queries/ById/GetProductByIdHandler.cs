using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Queries.ById;


public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ResultOf<ProductDTO>>
{
    private ProductContext context;
    public GetProductByIdHandler(ProductContext context)
    {
        this.context = context;
    }
    public async Task<ResultOf<ProductDTO>> Handle(GetProductByIdRequest query, CancellationToken cancellationToken)
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
