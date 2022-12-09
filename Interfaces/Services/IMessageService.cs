using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using UNIFY.Dtos;
using UNIFY.Model.Enums;

namespace Unify.UNIFY.Interfaces.Services
{
    public interface IMessageService
    {
        Task<BaseResponse> Create(CreateMessageRequestModel model);
        Task<BaseResponse> Update(UpdateMessageRequestModel model,string Id);
        Task<MessageResponseModel> Get(string Id);
        Task<MessageResponseModel> GetMessageByType(MessageType MessageType);
       Task<bool> Delete(string Id);
    }
}