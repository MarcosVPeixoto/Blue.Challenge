namespace Blue.Challenge.Domain.Entities
{
    public class User(string name, string email, string password)
    {
        public int Id { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set;  } = password;
        public IList<Contact> Contacts { get; set;  }

        public bool VerifyPassword(string password)
        {
            return PasswordHasher.VerifyPassword(password, Password);
        }
    }
}
