using System;
using UNIFY.Model.Base;

namespace Unify.UNIFY.Model.Entities
{
    public class PostLog : AuditableEntity
    {
        public string PostId{get;set;}
        public Post Post{get;set;}
        public string PostUrl{get;set;}
        public DateTime DateCreated { get; set; }
    }
}