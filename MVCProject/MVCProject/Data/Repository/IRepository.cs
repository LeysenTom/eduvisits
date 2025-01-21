namespace MVCProject.Data.repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByStringIdAsync(string id);
        void Save();

        IQueryable<TEntity> Search();

        void Update(TEntity entity);
    }
}