using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Update;

public partial class UpdateProductRequest
{
    class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest, ResultOf<int>>
    {
        private ProductContext context;

        public UpdateProductCommandHandler(ProductContext context)
        {
            this.context = context;
        }

        public async Task<ResultOf<int>> Handle(UpdateProductRequest command, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if (product is not null)
            {
                if (product.Price >= command.Price)
                {
                    return new BadRequestError().AddFieldErrors("Price", "You must increase the price");
                }
                product.Name = command.Name;
                product.Price = command.Price;
                await context.SaveChangesAsync(cancellationToken);
            }

            return product?.Id ?? default;
        }
    }
}
