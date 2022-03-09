using CrudCQRS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudCQRS.CQRS.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private ProductContext context;

            public UpdateProductCommandHandler(ProductContext context)
            {
                this.context = context;
            }

            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
                if (product is not null)
                {
                    product.Name = command.Name;
                    product.Price = command.Price;
                    await context.SaveChangesAsync(cancellationToken);
                }

                return product?.Id ?? default;
            }
        }
    }
}
