using CrudCQRS.DTO;
using MediatR;
using Nudes.Retornator.Core;

namespace CrudCQRS.Features.Product.Queries.All;

public class GetAllProductQueryRequest : IRequest<ResultOf<List<ProductDTO>>>
{
}
