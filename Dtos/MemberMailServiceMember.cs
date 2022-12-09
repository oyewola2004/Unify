using Model.Entities;
using UNIFY.Model.Entities;

namespace Dtos
{
    public class MemberMailServiceMember
    {
         public string MemberId {get;set;}
        public Member Member{get;set;}
        public string MemberMailServiceId{get;set;}
        public MemberMailService MemberMailService{get;set;}
    }
}