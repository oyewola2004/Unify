using UNIFY.Model.Base;
using UNIFY.Model.Entities;

namespace Model.Entities
{
    public class MemberMailServiceMember : AuditableEntity
    {
         public string MemberId {get;set;}
        public Member Member{get;set;}
        public string MemberMailServiceId{get;set;}
        public MemberMailService MemberMailService{get;set;}
    }
}