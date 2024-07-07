using Blue.Challenge.Domain.Entities;

namespace Blue.Challenge.Infra.Interfaces
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<Contact> GetByIdAsync(int id);
        Task<List<Contact>> GetByUserIdAsync(Guid userId);
    }
}
