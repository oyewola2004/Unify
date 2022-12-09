using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unify.UNIFY.Model.Entities;
using UNIFY.Interfaces.Repository;

namespace Unify.UNIFY.Interfaces.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetMemberPost(string id);
        Task<Post> GetPostById(string id);
        Task<IEnumerable<Post>> GetAllPost();  
        Task<IEnumerable<Post>> GetAllPosts(Expression<Func<Post, bool>> expression);  
        Task<IEnumerable<Post>> GetAllPostsToday();
        Task<IList<Post>> GetNotVerifiedReportedPosts();
        Task<IList<Post>> GetAllReportedPost();
        Task<IEnumerable<Post>> GetAllPostByCategory(string CategoryId);
    }
}