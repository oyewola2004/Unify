using System.Collections.Generic;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace Unify.UNIFY.Interfaces.Repository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment> GetMemberComment(string Id);
        Task<IEnumerable<Comment>> GetMemberComments();
        Task<IEnumerable<Comment>> GetCommentsOfAPost(string PostId);
        Task<Comment> GetById(string id);
        //Task<BaseResponse> GetCommentsOfAPost(string PostId);
    }
}