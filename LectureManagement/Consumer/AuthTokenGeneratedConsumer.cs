using Contracts;
using MassTransit;

namespace LectureManagement.Consumer
{
    public sealed class AuthTokenGeneratedConsumer : IConsumer<AuthTokenGeneratedEvent>
    {
        public static string Token = null;
        public Task Consume(ConsumeContext<AuthTokenGeneratedEvent> context)
        {
            var token = context.Message.Token;
            Console.WriteLine($"Token generated: {token}");
            Token = token;
            return Task.CompletedTask;
        }
    }
}
