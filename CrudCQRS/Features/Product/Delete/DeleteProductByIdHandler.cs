using CrudCQRS.Models;
using MediatR;

using Nudes.Retornator.AspnetCore.Errors;


namespace CrudCQRS.Features.Product.Delete;


public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest, Result>
{
    private readonly ProductContext context;
    public DeleteProductByIdHandler(ProductContext context)
    {
        this.context = context;
    }

    public async Task<Result> Handle(DeleteProductByIdRequest command, CancellationToken cancellationToken)
    {
        var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
        if (product == null)
            return new NotFoundError();

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}

