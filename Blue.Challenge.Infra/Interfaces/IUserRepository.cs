using Blue.Challenge.Domain.Entities;

namespace Blue.Challenge.Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUserId(Guid userId);
    }
}
