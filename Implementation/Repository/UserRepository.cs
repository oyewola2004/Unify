using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UNIFY.Context;
using UNIFY.Identity;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(string id)
        {
            var user = await _context.Users.Include(x => x.Member).FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<Member> GetMemberByUserId(string UserId)
        {
            var member = await _context.Members.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == UserId);
            return member;
        }
    }
}
