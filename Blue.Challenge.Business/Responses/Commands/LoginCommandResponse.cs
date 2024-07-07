namespace Blue.Challenge.Business.Responses.Commands
{
    public record LoginCommandResponse
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset? AccessTokenExpireDate { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
