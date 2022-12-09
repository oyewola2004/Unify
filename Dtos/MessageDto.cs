using System.Collections.Generic;
using UNIFY.Dtos;
using UNIFY.Model.Enums;

namespace Unify.UNIFY.Dtos
{
    public class MessageDto
    {
         public string Id{get;set;}
        public string MessageContent{get;set;}
        public  MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
    }
    public class UpdateMessageRequestModel 
    {
        public string MessageContent{get;set;}
        public  MessageType MessageType{get;set;}
    }
    public class CreateMessageRequestModel 
    {
        public string MessageContent{get;set;}
        public int MessageType{get;set;}
        public string MessageSubject{get;set;}
    }


      public class MessageResponseModel : BaseResponse
    {
        public MessageDto Data { get; set; }
    }
    public class MessagesResponseModel : BaseResponse
    {
        public ICollection<MessageDto> Data { get; set; } = new HashSet<MessageDto>();
    }
    
}