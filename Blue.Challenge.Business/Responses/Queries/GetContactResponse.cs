namespace Blue.Challenge.Business.Responses.Queries
{
    public record GetContactResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Id { get; set; }
    }
}
