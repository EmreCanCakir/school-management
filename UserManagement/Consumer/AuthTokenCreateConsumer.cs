using UserManagement.Services;
using Contracts;
using MassTransit;

namespace UserManagement.Consumer
{
    public sealed class AuthTokenCreateConsumer : IConsumer<AuthTokenCreateEvent>
    {
        private readonly ITokenService _tokenService;
        private readonly IPublishEndpoint _publishEndpoint;
        public AuthTokenCreateConsumer(ITokenService tokenService, IPublishEndpoint publishEndpoint)
        {
            _tokenService = tokenService;
            _publishEndpoint = publishEndpoint;
        }

        public Task Consume(ConsumeContext<AuthTokenCreateEvent> context)
        {
            var token = _tokenService.GenerateJwtToken(context.Message.UserName, context.Message.Password);
            _publishEndpoint.Publish(new AuthTokenGeneratedEvent { Token = token}, context.CancellationToken);
            return Task.CompletedTask;
        }
    }
}
