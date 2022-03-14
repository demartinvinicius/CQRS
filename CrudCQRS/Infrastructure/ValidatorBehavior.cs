using Nudes.Retornator.AspnetCore.Errors;

namespace CrudCQRS.Infrastructure;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators=validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var errors = validators.Select(d => d.Validate(request))
            .Where(d => !d.IsValid);

        if (errors.Any())
        {
            var result = Activator.CreateInstance<TResponse>();

            if (result is Nudes.Retornator.Core.IResult res)
            {
                res.Error = new BadRequestError()
                {
                    FieldErrors = new FieldErrors(errors
                        .SelectMany(d => d.Errors)
                        .GroupBy(d => d.PropertyName)
                        .ToDictionary(d => d.Key, d => d.Select(d => d.ErrorMessage)))

                };
            }

            return Task.FromResult(result);
        }
        else
            return next();
    }
}
