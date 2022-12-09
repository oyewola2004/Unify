using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Model.Entities;
using UNIFY.Context;
using UNIFY.Implementation.Repository;

namespace Unify.UNIFY.Implementation.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostByCategory(string CategoryId)
        {
            return await _context.Posts.Include(x => x.Category)
            .Include(x => x.Member)
            .ThenInclude(x => x.User).Include(x => x.Comments)
            .Where( x => x.CategoryId == CategoryId && x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPost()
        {
            return await _context.Posts.Include(L=> L.Member)
            .ThenInclude(L=> L.User).Include(L=> L.Comments)
            .Include(L=> L.PostLog)
            .Where(L => L.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPosts(Expression<Func<Post, bool>> expression)
        {
           return await _context.Posts.Include(L=> L.Member)
           .ThenInclude(L=> L.User).Include(L=> L.Comments)
           .Include(L=> L.PostLog).Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsToday()
        {
            return await _context.Posts.Include(L=> L.Member)
            .ThenInclude(L=>L.User).Include(L=> L.Comments)
            .Where(L=> L.DatePosted.Date == DateTime.UtcNow.Date && L.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Post>> GetAllReportedPost()
        {
           var posts = await _context.Posts.Include(x => x.User).Include(x => x.Member)
            .Where(x => x.IsDeleted == false && x.ISReported == true).ToListAsync();
            return posts;
        }

        public async Task<Post> GetMemberPost(string id)
        {
           return await _context.Posts.Include(L=> L.Member)
           .ThenInclude(L=>L.User).Include(L=> L.Comments)
           .Include(L=> L.PostLog).SingleOrDefaultAsync(L=> L.Id == id && L.IsDeleted == false);
        }

        public async Task<IList<Post>> GetNotVerifiedReportedPosts()
        {
            var posts = await _context.Posts.Include(x => x.User)
            .Where(x => x.IsDeleted == false && x.ISReported == false).ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostById(string id)
        {
            var post = await _context.Posts.Include(x => x.Category).Include(x => x.Comments).Include(x => x.User)
            .Where(x => x.IsDeleted == false).SingleOrDefaultAsync(x => x.Id == id);
            return post;
        }
    }
}