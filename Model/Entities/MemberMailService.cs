using System;
using System.Collections.Generic;
using UNIFY.Model.Base;
using UNIFY.Model.Enums;

namespace Model.Entities
{
    public class MemberMailService : AuditableEntity
    {
        public string MessageContent{get;set;}
        public MessageType MessageType{get;set;}
        public string MessageSubject{get;set;}
        public DateTime DateSent{get;set;}

         public ICollection<MemberMailServiceMember> MemberMailServiceMember{get;set;}
    }
}