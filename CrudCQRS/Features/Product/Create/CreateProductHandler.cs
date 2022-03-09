﻿using CrudCQRS.Models;
using MediatR;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Create;

class CreateProductHandler : IRequestHandler<CreateProductRequest, ResultOf<int>>
{
    private readonly ProductContext context;

    public CreateProductHandler(ProductContext context)
    {
        this.context=context;
    }
    public async Task<ResultOf<int>> Handle(CreateProductRequest command, CancellationToken cancellationToken)
    {
        var product = new Models.Product
        {
            Name = command.Name,
            Price = command.Price
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}