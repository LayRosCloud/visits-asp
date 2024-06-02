using LearnAsp.Domain;

namespace LearnAsp.Repository
{
    public interface IProblemRepository
    {
        Task<IEnumerable<Problem>> GetAllAsync();
    }
}
