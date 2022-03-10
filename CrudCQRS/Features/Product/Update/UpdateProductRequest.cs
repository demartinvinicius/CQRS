using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Update;

public partial class UpdateProductRequest : IRequest<ResultOf<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
