using LearnAsp.Application;
using LearnAsp.Domain;
using LearnAsp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LearnAsp.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByLogin(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
            if (user == null)
            {
                throw new NotFoundException();
            }
            return user;
        }
    }
}
