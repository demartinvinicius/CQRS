using CrudCQRS.DTO;
using MediatR;
using Nudes.Retornator.Core;


namespace CrudCQRS.Features.Product.Queries.ById;

public class GetProductByIdRequest : IRequest<ResultOf<ProductDTO>>
{
    public int Id { get; set; }
}
