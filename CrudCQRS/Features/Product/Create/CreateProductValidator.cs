namespace CrudCQRS.Features.Product.Create;

class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(d => d.Product.Name).NotEmpty();
        RuleFor(d => d.Product.Price).GreaterThanOrEqualTo(0);
    }
}
