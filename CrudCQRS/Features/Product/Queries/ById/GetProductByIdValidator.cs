using FluentValidation;

namespace CrudCQRS.Features.Product.Queries.ById;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdValidator()
    {
        RuleFor(d => d.Id).GreaterThan(0);
    }
}
