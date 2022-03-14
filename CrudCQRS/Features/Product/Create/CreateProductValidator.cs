using FluentValidation;

namespace CrudCQRS.Features.Product.Create;

class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(d => d.product.Name).NotEmpty();
        RuleFor(d => d.product.Price).GreaterThanOrEqualTo(0);
    }
}
