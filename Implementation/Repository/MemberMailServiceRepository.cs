using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using UNIFY.Context;
using UNIFY.Implementation.Repository;

namespace Implementation.Repository
{
    public class MemberMailServiceRepository : BaseRepository<MemberMailService>, IMemberMailServiceRepository
    {
         public MemberMailServiceRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MemberMailService>> GetAdminMessages()
        {
            return await _context.MemberMailService
            .Include(L=> L.MemberMailServiceMember).ToListAsync();
        }

        public async Task<MemberMailService> GetMemberMessage(string Id)
        {
             var memberMail = await _context.MemberMailService
           .Include(L=> L.MemberMailServiceMember)
           .SingleOrDefaultAsync(L=> L.Id ==Id);
           return memberMail;
        }
    }
}