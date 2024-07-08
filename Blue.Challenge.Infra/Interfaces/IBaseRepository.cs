namespace Blue.Challenge.Infra.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {
        Task AddAsync<T>(T entity);
        void Remove<T>(T entity);
        void Update<T>(T entity);
        Task SaveChangesAsync();
    }
}
