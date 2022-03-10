using FluentValidation;

namespace CrudCQRS.Features.Product.Bogus;

public class CreateBogusValidator : AbstractValidator<CreateBogusRequest>
{
    public CreateBogusValidator()
    {
        RuleFor(d => d.NumberItensToGenerate).GreaterThanOrEqualTo(0);
        RuleFor(d => d.NumberItensToGenerate).LessThanOrEqualTo(1000);

    }
}
