using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using UNIFY.Dtos;

namespace Unify.UNIFY.Interfaces.Services
{
    public interface IPostLogService
    {
         Task<BaseResponse> CreatePostLog(CreatePostLogRequestModel model);
    }
}