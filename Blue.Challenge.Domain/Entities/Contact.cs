namespace Blue.Challenge.Domain.Entities
{
    public class Contact(string name, string email, string phone, int userId)
    {
        public int Id { get; set; }
        public int UserId { get; set; } = userId;
        public User User { get; set; }
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Phone { get; set; } = phone;
    }
}
