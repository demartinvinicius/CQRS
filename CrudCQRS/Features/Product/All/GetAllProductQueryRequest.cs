namespace CrudCQRS.Features.Product.Queries.All;

public class GetAllProductQueryRequest : PageRequest, IRequest<ResultOf<PageResult<ProductDTO>>>
{
    public decimal? MinimumPrice { get; set; }
    public decimal? MaximumPrice { get; set; }


}
