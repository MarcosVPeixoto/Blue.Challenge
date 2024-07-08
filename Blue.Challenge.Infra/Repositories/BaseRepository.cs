using Blue.Challenge.Infra.Context;
using Blue.Challenge.Infra.Interfaces;

namespace Blue.Challenge.Infra.Repositories
{
    public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context = context;

        public async Task AddAsync<T>(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void Remove<T>(T entity)
        {
            _context.Remove(entity);
        }
        
        public void Update<T>(T entity)
        {
            _context.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
