namespace Contracts
{
    public record AuthTokenCreateEvent
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
