using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Create;

public class CreateProductRequest : IRequest<ResultOf<int>>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
