using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using UNIFY.Dtos;

namespace Unify.UNIFY.Interfaces.Services
{
    public interface ICommentService
    {
        Task<BaseResponse> CreateComment(CreateCommentRequestModel model,string MemberId , string PostId);
        Task<BaseResponse> UpdateComment(UpdateCommentRequestModel model, string Id);
        Task<BaseResponse> Get(string Id);
        Task<BaseResponse> Get();
       Task<CommentsResponseModel> GetCommentsOfAPost(string PostId);
        Task<bool> Delete(string Id);
    }
}