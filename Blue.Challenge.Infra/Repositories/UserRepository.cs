using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Context;
using Blue.Challenge.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blue.Challenge.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
                
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByUserId(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
