using System;
using System.Collections.Generic;
using UNIFY.Model.Enums;

namespace Dtos
{
    public class MemberMailServiceDto
    {
         public string Id{get;set;}
        public string MessageContent{get;set;}
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
        public DateTime DateSent{get;set;}
        public ICollection<MemberMailServiceMember> MemberMailServiceMember{get;set;}
    }

     public class CreateMemberMailServiceRequestModel 
    {
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}

    }
    public class UpdateMemberMailServiceRequestModel 
    {
    
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
        public DateTime DateSent{get;set;}
        
    }
}