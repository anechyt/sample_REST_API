using FluentValidation;
using Mediator;

namespace SampleRestApi.Application.Infrastructure
{
    public class RequestValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
        where TMessage : IMessage
    {
        private readonly IMediator _mediator;
        private readonly IEnumerable<IValidator<TMessage>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TMessage>> validators, IMediator mediator)
        {
            _mediator = mediator;
            _validators = validators;
        }

        public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken, MessageHandlerDelegate<TMessage, TResponse> next)
        {
            var context = new ValidationContext<TMessage>(message);

            var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failures => failures != null)
            .ToList();

            if (failures.Any())
            {
                await _mediator.Publish(new Exception("Error handling message"));
                throw new ValidationException(failures);
            }

            return await next(message, cancellationToken);
        }
    }
}
