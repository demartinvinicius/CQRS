using FluentValidation;

namespace CrudCQRS.Features.Product.Update;

class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
        RuleFor(d => d.Price).GreaterThanOrEqualTo(0);
    }
    
}
