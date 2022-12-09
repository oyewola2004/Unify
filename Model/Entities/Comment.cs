using System;
using Unify.UNIFY.Model.Entities;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Comment : AuditableEntity
    {
        public string CommentId { get; set; }
        public string CommentText { get; set; }
        public string MemberId {get;set;}
        public Member Member{get;set;}
        public string PostId{get;set;}
        public Post Post{get;set;}
        public DateTime CommentDate{get;set;}
         public DateTime DateCreated{get;set;}
    }
}
