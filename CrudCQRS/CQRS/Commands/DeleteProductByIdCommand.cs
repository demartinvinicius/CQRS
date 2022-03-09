using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudCQRS.CQRS.Commands
{
    public class DeleteProductByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
        {
            private ProductContext context;
            public DeleteProductByIdCommandHandler(ProductContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
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
}
