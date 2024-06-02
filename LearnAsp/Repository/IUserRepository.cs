using LearnAsp.Domain;

namespace LearnAsp.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByLogin(string login);
    }
}
