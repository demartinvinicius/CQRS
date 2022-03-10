using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Update;

public partial class UpdateProductCommand
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, ResultOf<int>>
    {
        private ProductContext context;

        public UpdateProductHandler(ProductContext context)
        {
            this.context = context;
        }

        public async Task<ResultOf<int>> Handle(UpdateProductRequest command, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if (product == null)
                return new NotFoundError();

            
            product.Name = command.Name;
            product.Price = command.Price;
            await context.SaveChangesAsync(cancellationToken);
            

            return product.Id;
        }
    }
}
