using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Update;


public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Result>
{
    private readonly ProductContext context;

    public UpdateProductHandler(ProductContext context)
    {
        this.context = context;
    }

    public async Task<Result> Handle(UpdateProductRequest command, CancellationToken cancellationToken)
    {
        var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
        if (product == null)
            return new NotFoundError();

        if (product.Price >= command.Price)
        {
            return new BadRequestError().AddFieldErrors("Price", "You must increase the price");
        }

        product.Name = command.Name;
        product.Price = command.Price;
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}

