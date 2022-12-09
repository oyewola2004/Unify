using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Interfaces.Services;
using UNIFY.Dtos;
using UNIFY.Model.Entities;
using UNIFY.Model.Enums;

namespace Unify.UNIFY.Implementation.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<BaseResponse> Create(CreateMessageRequestModel model)
        {
             var message = new Message
            {
               MessageType = (MessageType)model.MessageType,
                MessageContent = model.MessageContent,
                MessageSubject = model.MessageSubject
                 
            };
             await _messageRepository.Register(message);
            return new BaseResponse
            {
               Message = "Creation Successful",
               Status = true,
            };
        }

        public async Task<bool> Delete(string Id)
        {
             var message = await _messageRepository.Get(a => a.Id == Id);
           if(message==null)
           {
               return false;
           } 
            message.IsDeleted =true;
            await _messageRepository.Delete(message);
            return true;
        }

        public async Task<MessageResponseModel> Get(string Id)
        {
             var message = await _messageRepository.Get(a => a.Id == Id);
            if(message == null) return new MessageResponseModel 
            {
                Message = $"Message With Id {Id} Not Found ",
                Status = false,
            };
             var messages = new MessageDto
            {
               MessageType = message.MessageType,
                MessageContent = message.MessageContent,
                MessageSubject = message.MessageSubject
                 
            };
            return new MessageResponseModel
            {
                Data = messages,
                Message = " Retrieval Success",
                Status = true,
               
            };
        }

        public async Task<MessageResponseModel> GetMessageByType(MessageType MessageType)
        {
            var message = await _messageRepository.GetMessageByType(MessageType);
            if(message == null) return new MessageResponseModel
            {
                Message = $"Message With Type {MessageType} Not Found ",
                Status = false,
            };
              var messages = new MessageDto
            {
               MessageType = message.MessageType,
                MessageContent = message.MessageContent,
                MessageSubject = message.MessageSubject
                 
            };
            return new MessageResponseModel
            {
                Data = messages,
                Message = $"Message With Type {MessageType} Found ",
                Status = true,
                
            };
        }

        public async Task<BaseResponse> Update(UpdateMessageRequestModel model, string Id)
        {
            var message = await _messageRepository.Get(a => a.Id == Id);
            if(message == null) return new BaseResponse
            {
                Message = $"Message With Id {Id} Not Found ",
                Status = false,
            };
              message.MessageType = model.MessageType;
              message.MessageContent = model.MessageContent;
              await _messageRepository.Update(message);
            return new BaseResponse
            {
                Message = $"Message With Id {Id} Found ",
                Status = true,
                
            };
        }
    }
}