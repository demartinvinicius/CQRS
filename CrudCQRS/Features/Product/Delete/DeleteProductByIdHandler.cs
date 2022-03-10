using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Delete;

public partial class DeleteProductByIdRequest
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest, ResultOf<int>>
    {
        private ProductContext context;
        public DeleteProductByIdHandler(ProductContext context)
        {
            this.context = context;
        }

        public async Task<ResultOf<int>> Handle(DeleteProductByIdRequest command, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if (product == null)
                return default;

            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
