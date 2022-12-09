using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unify.UNIFY.Interfaces.Repository;
using UNIFY.Context;
using UNIFY.Implementation.Repository;
using UNIFY.Model.Entities;

namespace Unify.UNIFY.Implementation.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
         public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }
       

        public async Task<IEnumerable<Comment>> GetCommentsOfAPost(string PostId)
        {
             return await _context.Comments.Include(L=> L.Member)
            .ThenInclude(L=> L.User).Include(L=> L.Post)
            .Where(L=> L.PostId.Equals(PostId)  && L.IsDeleted == false).ToListAsync();
        }

        public async Task<Comment> GetMemberComment(string Id)
        {
           return await _context.Comments.Include(L=> L.Member)
           .ThenInclude(L=> L.User).Include(L=> L.Post)
           .SingleOrDefaultAsync(L=> L.Id ==Id);
        }

        public async Task<IEnumerable<Comment>> GetMemberComments()
        {
             return await _context.Comments.Include(L=> L.Member)
             .ThenInclude(L=> L.User).Include(L=> L.Post).ToListAsync();
        }

         public async Task<Comment> GetById(string id)
        {
            var post = await _context.Comments.Include(x => x.Post)
            .FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }
    }
}