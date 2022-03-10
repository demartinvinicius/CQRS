using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Update;

public class UpdateProductRequest : IRequest<Result>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
