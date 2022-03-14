namespace CrudCQRS.Features.Product.Create;

public class CreateProductRequest : IRequest<ResultOf<int>>
{
    public ProductDTO Product { get; set; }
}
