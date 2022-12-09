using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using UNIFY.Dtos;

namespace Unify.UNIFY.Interfaces.Services
{
    public interface IPostService
    {
        Task<PostResponseModel> Create(CreatePostRequestModel model, string MemberId);
        Task<PostResponseModel> Update(UpdatePostRequestModel model, string Id);
        Task<PostResponseModel> Get(string Id);
        Task<PostsResponseModel> Get();
        Task<PostsResponseModel> GetPostsForToday();
        Task<PostsResponseModel> GetPostsOfUser(string UserId);
        Task<CategoriesResponseModel> GetAllPostByCategory(string CategoryId);
        Task<BaseResponse> ReportPost(string id, string UserId);
        Task<PostsResponseModel> GetNotVerifiedReportedPosts();
        Task<PostsResponseModel> GetAllReportedPost();
        Task<bool> Delete(string Id);
    }
}