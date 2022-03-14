using CrudCQRS.DTO;
using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Create;

public class CreateProductRequest : IRequest<ResultOf<int>>
{
    public ProductDTO product { get; set; }
}
