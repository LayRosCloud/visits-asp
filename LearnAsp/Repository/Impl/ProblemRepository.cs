using LearnAsp.Application;
using LearnAsp.Domain;
using Microsoft.EntityFrameworkCore;

namespace LearnAsp.Repository.Impl
{
    public class ProblemRepository : IProblemRepository
    {

        private readonly DatabaseContext _context;
        public ProblemRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Problem>> GetAllAsync()
        {
            var items = await _context.Problems.ToListAsync();
            return items;
        }
    }
}
