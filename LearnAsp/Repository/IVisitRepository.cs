using LearnAsp.Domain;

namespace LearnAsp.Repository
{
    public interface IVisitRepository
    {
        Task<IEnumerable<Visit>> GetAllAsync(Guid userId);
        Task<Visit> GetByIdAsync(int id);
        Task<Visit> AddAsync(Visit visit);
        Task<Visit> UpdateAsync(Visit visit);
        Task Save();
    }
}
