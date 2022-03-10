using FluentValidation;

namespace CrudCQRS.Features.Product.Queries.All;

public class GetAllProductQueryValidator : AbstractValidator<GetAllProductQueryRequest>
{
    public GetAllProductQueryValidator()
    {
        RuleFor(d => d.MinimumPrice).GreaterThanOrEqualTo(0);
        RuleFor(d => d.MaximumPrice).GreaterThanOrEqualTo(0);
    }
}
