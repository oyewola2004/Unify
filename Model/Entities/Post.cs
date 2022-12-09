using System;
using System.Collections.Generic;
using UNIFY.Identity;
using UNIFY.Model.Base;
using UNIFY.Model.Entities;

namespace Unify.UNIFY.Model.Entities
{
    public class Post : AuditableEntity
    {
        public string MemberId {get;set;}
        public Member Member{get;set;}
        public string UserId {get; set; }
        public User User {get; set; }
        public string PostContent{get;set;}
        public bool ISReported {get; set;}
        public string? VideoFile {get;set;}
        public DateTime DatePosted{get;set;}
        public PostLog PostLog {get;set;}
        public string CategoryId {get; set;}
        public Category Category {get; set;}
        public ICollection<Comment>Comments{get;set;}
        public string Title {get;set;}
    }
}