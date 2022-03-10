using CrudCQRS.DTO;
using MediatR;
using Nudes.Retornator.Core;
using Nudes.Paginator.Core;

namespace CrudCQRS.Features.Product.Queries.All;

public class GetAllProductQueryRequest : PageRequest, IRequest<ResultOf<List<ProductDTO>>> 
{
    public decimal? MinimumPrice { get; set; }
    public decimal? MaximumPrice { get; set; }


}
