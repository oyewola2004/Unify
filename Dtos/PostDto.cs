using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using UNIFY.Dtos;

namespace Unify.UNIFY.Dtos
{
    public class PostDto
    {
        public string PostId{get;set;}
        public string UserId{get;set;}
        public string CategoryName{get; set; }
        public string Email{get;set;}
        public string UserName{get;set;}
        public string Title{get;set;}
        public string CreatorPhoto{get;set;}
        public string PostContent{get;set;}
        public string? VideoFile{get;set;}
        public DateTime DatePosted{get;set;}
        public ICollection<CommentDto> Comments{get;set;} = new List<CommentDto>();
   
    }


    public class CreatePostRequestModel
    {
        public string Title{get;set;}
        public string PostContent{get;set;}
        public string VideoFile {get;set;}
        public string CategoryId {get; set;}
    }

    public class UpdatePostRequestModel
    {
        public string PostContent{get;set;}
        public string PostTitle{get;set;}
    
    }

    public class PostResponseModel : BaseResponse
        {
            public PostDto Data { get; set; }
        }
        public class PostsResponseModel : BaseResponse
        {
            public ICollection<PostDto> Data { get; set; } = new HashSet<PostDto>();
        }
}