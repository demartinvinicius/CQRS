using FluentValidation;

namespace CrudCQRS.Features.Product.Delete;

class DeleteProductByIdValidator : AbstractValidator<DeleteProductByIdRequest>
{
    public DeleteProductByIdValidator()
    {
        RuleFor(d => d.Id).GreaterThan(0);
    }
}
