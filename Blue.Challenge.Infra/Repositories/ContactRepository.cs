using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Context;
using Blue.Challenge.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blue.Challenge.Infra.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts.Include(x => x.User)
                                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Contact>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Users.Where(x => x.UserId == userId)
                                       .SelectMany(x => x.Contacts)
                                       .ToListAsync();
        }
    }
}
