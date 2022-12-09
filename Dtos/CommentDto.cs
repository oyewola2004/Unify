using System;
using System.Collections.Generic;
using UNIFY.Dtos;

namespace Unify.UNIFY.Dtos
{
    public class CommentDto
    {
        public string Id{get;set;}
        public string MemberId{get;set;}
        public string MemberName{get;set;}
        public string CommentText{get;set;}
        public string PostId{get;set;}
        public string PostCreator{get;set;}
        public DateTime PostDate{get;set;}
        public DateTime CommentDate{get;set;}
    }

     public class CreateCommentRequestModel 
    {
        public string CommentText{get;set;}
        public string PostId{get;set;}
    }
    public class UpdateCommentRequestModel 
    {
        public string CommentText{get;set;}
    }

     public class CommentResponseModel : BaseResponse
        {
            public CommentDto Data { get; set; }
        }
        public class CommentsResponseModel : BaseResponse
        {
            public ICollection<CommentDto> Data { get; set; } = new HashSet<CommentDto>();
        }
}