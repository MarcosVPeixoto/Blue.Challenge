namespace Blue.Challenge.Domain.Configuration
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int AccessTokenExpireTime { get; set; }
    }
}
