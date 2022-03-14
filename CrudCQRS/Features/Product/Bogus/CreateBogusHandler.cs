using Bogus;
using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Nudes.Retornator.Core;
using Mapster;

namespace CrudCQRS.Features.Product.Bogus;

public class CreateBogusHandler : IRequestHandler<CreateBogusRequest, ResultOf<List<ProductDTO>>>
{
    private readonly ProductContext context;

    public CreateBogusHandler(ProductContext context) =>
        this.context = context;


    public async Task<ResultOf<List<ProductDTO>>> Handle(CreateBogusRequest request, CancellationToken cancellationToken)
    {
        if (request.EraseDatabase.HasValue && request.EraseDatabase.Value)
        {
            context.Products.RemoveRange(context.Products.Select(a => a));
            await context.SaveChangesAsync(cancellationToken);
        }

        var bogusproducts = new Faker<Models.Product>()
                .StrictMode(false)
                .RuleFor(o => o.Price, f => decimal.Round(f.Random.Decimal(10, 1000), 2))
                .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                .Generate(request.NumberItensToGenerate ?? 100);


        bogusproducts.ForEach(async d => await context.Products.AddAsync(d, cancellationToken));

        await context.SaveChangesAsync(cancellationToken);

        return bogusproducts.AsQueryable<Models.Product>().ProjectToType<ProductDTO>().ToList();
        
    }
}
