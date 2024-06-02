using LearnAsp.Application;
using LearnAsp.Domain;
using LearnAsp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LearnAsp.Repository.Impl
{
    public class VisitRepository : IVisitRepository
    {
        private readonly DatabaseContext _context;

        public VisitRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Visit> AddAsync(Visit visit)
        {
            visit.CreatedAt = DateTime.Now;
            visit.Problem = null;
            visit.User = null;
            var savedVisit = await _context.Visits.AddAsync(visit);
            await Save();
            return savedVisit.Entity;
        }

        public async Task<IEnumerable<Visit>> GetAllAsync(Guid userId)
        {
            var visits = await _context.Visits
                .Where(x => x.UserId == userId)
                .Include(x => x.User)
                .Include(x => x.Problem)
                .ToListAsync();
            return visits;
        }

        public async Task<Visit> GetByIdAsync(int id)
        {
            var visit = await _context.Visits
               .Include(x => x.User)
               .Include(x => x.Problem)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (visit == null)
            {
                throw new NotFoundException();
            }

            return visit;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Visit> UpdateAsync(Visit visit)
        {
            var savedVisit = _context.Update(visit);
            await Save();
            return savedVisit.Entity;
        }
    }
}
