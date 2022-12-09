using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Model.Entities;
using UNIFY.Context;
using UNIFY.Implementation.Repository;

namespace Unify.UNIFY.Implementation.Repository
{
    public class PostLogRepository : BaseRepository<PostLog>, IPostLogRepository
    {
        public PostLogRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostLog>> GetLogsOfPostCreatedToday()
        {
            return await _context.PostLogs.Include(L=> L.Post)
            .Where(L=> L.Post.DatePosted.Date == System.DateTime.UtcNow.Date).ToListAsync();
        }
    }
}