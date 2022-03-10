﻿using Bogus;
using CrudCQRS.DTO;
using CrudCQRS.Models;
using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Bogus;

public class CreateBogusHandler : IRequestHandler<CreateBogusRequest, ResultOf<List<ProductDTO>>>
{
    private ProductContext context;

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

        foreach (var product in bogusproducts)
        {
            await context.Products.AddAsync(product, cancellationToken);
        }
        await context.SaveChangesAsync(cancellationToken);

        return bogusproducts.Select(a => new ProductDTO
        {
            Id = a.Id,
            Name = a.Name,
            Price = a.Price,
        }).ToList();
    }
}