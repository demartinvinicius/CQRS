using FluentValidation;

namespace CrudCQRS.Features.Product.Create;

class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
        RuleFor(d => d.Price).GreaterThanOrEqualTo(0);
    }
}
