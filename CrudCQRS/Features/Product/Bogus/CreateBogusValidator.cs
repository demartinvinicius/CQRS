using FluentValidation;

namespace CrudCQRS.Features.Product.Bogus;

public class CreateBogusValidator : AbstractValidator<CreateBogusRequest>
{
    public CreateBogusValidator()
    {
        RuleFor(d => d.NumberItensToGenerate).InclusiveBetween(1, 1000);
    }
}
