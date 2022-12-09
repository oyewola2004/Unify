using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using UNIFY.Dtos;

namespace UNIFY.Interfaces.Services
{
    public interface ISecurityAgencyService
    {
        Task<BaseResponse> Create(CreateSecurityAgencyRequestModel model);
        Task<BaseResponse> Update(string id, UpdateSecurityAgencyRequestModel model);
        Task<BaseResponse> Get(string id);
        Task<BaseResponse> GetAll();
        Task<BaseResponse> Delete(string id);
    }
}
