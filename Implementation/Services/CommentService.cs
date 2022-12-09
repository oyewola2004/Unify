using System;
using System.Linq;
using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Interfaces.Services;
using UNIFY.Dtos;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace Unify.UNIFY.Implementation.Services
{
    public class CommentService : ICommentService
    {

        private readonly ICommentRepository _CommentRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IPostRepository _PostRepository;
        private readonly IMemberRepository _memberRepository;

        public CommentService(ICommentRepository CommentRepository,IMemberRepository memberRepository, IUserRepository UserRepository,IPostRepository PostRepository)
        {
            _CommentRepository = CommentRepository;
            _UserRepository = UserRepository;
            _PostRepository = PostRepository;
            _memberRepository = memberRepository;
        }

        public async Task<BaseResponse> CreateComment(CreateCommentRequestModel model, string MemberId, string PostId)
        {
            System.Console.WriteLine(PostId);
            System.Console.WriteLine(MemberId);
            var userInPost = await _memberRepository.Get(x => x.UserId == MemberId);
          
          
             if(userInPost == null)
            {
                return new CommentResponseModel
                {
                    Message = "Member Id not found",
                    Status = false
                };
            }
             var appUser = userInPost;
             var Post = await _PostRepository.GetMemberPost(model.PostId);
             System.Console.WriteLine(Post);
             if(Post == null)
            {
                return new CommentResponseModel
                {
                    Message = "Post Id not found",
                    Status = false
                };
            }
           var comment = new  Comment
           {
               Member = appUser,
               MemberId = appUser.Id,
               CommentText = model.CommentText,
               CommentDate = DateTime.UtcNow,
               DateCreated = DateTime.UtcNow,
               PostId = model.PostId,
                 Post = Post,

           };
           await _CommentRepository.Register(comment);
           return new BaseResponse
           {
              Message = " Successful Initialization",
              Status = true,
             
           };
        }

        public async Task<bool> Delete(string Id)
        {
             var comment = await _CommentRepository.GetMemberComment(Id);
           if(comment == null) return false;
           comment.IsDeleted = true;
          await _CommentRepository.Delete(comment);
           return true;
        }

        public async Task<BaseResponse> Get(string Id)
        {
            var comment = await _CommentRepository.GetMemberComment(Id);
           if(comment == null) return new BaseResponse
           {
                  Message = "Could Not Fetch",
                  Status = true,
           };
           var commentDto = new CommentDto
           {
               Id = comment.Id,
                MemberId = comment.MemberId,
                MemberName = comment.Member.User.UserName,
               CommentText = comment.CommentText,
               CommentDate = comment.CommentDate,
               PostCreator = comment.Post.Member.User.UserName,
               PostDate = comment.Post.DatePosted,
               PostId = comment.PostId,
                  
           };
           return new BaseResponse
           {
              Message = "Successfully gotten",
              Status = true,
              

           };
        }

        public async Task<BaseResponse> Get()
        {
            var  applicationUserComments = await _CommentRepository.GetMemberComments();
           var applicationUserCommentsReturned = applicationUserComments.Select(comment => new CommentDto
           {
                  Id = comment.Id,
                MemberId = comment.MemberId,
                MemberName = comment.Member.User.UserName,
               CommentText = comment.CommentText,
               CommentDate = comment.CommentDate,
               PostCreator = comment.Post.Member.User.UserName,
               PostDate = comment.Post.DatePosted,
               PostId = comment.PostId,
           }).ToList();
           return new BaseResponse
           { 
               Message = "Successful Retrieval",
               Status = true,
               
           };
        }

        

        public async Task<BaseResponse> GetCommentsOfAPost(string PostId)
        {
            var postComments = await _CommentRepository.GetCommentsOfAPost( PostId);
           if(postComments == null)return new BaseResponse
           {    
                     Message = "Post Has No Comments yet",
                     Status = false,

           };
           var applicationUserCommentsReturned = postComments.Select(comment => new CommentDto
           {
                  Id = comment.Id,
                MemberId = comment.MemberId,
                // MemberName = comment.Member.User.UserName,
               CommentText = comment.CommentText,
               CommentDate = comment.CommentDate,
            //    PostCreator = comment.Post.Member.User.UserName,
               PostDate = comment.Post.DatePosted,
               PostId = comment.PostId,
           }).ToList();

           return new BaseResponse
           { 
               Message = "Successful Retrieval",
               Status = true,

           };
        }

        public async Task<BaseResponse> UpdateComment(UpdateCommentRequestModel model, string Id)
        {
             var comment = await _CommentRepository.GetMemberComment(Id);
             if(comment == null) return new BaseResponse
             {
                 Message = " Comment Not Found",
                 Status = false,
             };

              var commentDto = new CommentDto
           {
               Id = comment.Id,
                MemberId = comment.MemberId,
                MemberName = comment.Member.User.UserName,
               CommentText = comment.CommentText,
               CommentDate = comment.CommentDate,
               PostCreator = comment.Post.Member.User.UserName,
               PostDate = comment.Post.DatePosted,
               PostId = comment.PostId,
                  
           };

             comment.CommentText = comment.CommentText ?? model.CommentText;
             await _CommentRepository.Update(comment);
             return new BaseResponse
             {
                  Message = "Success In Updating",
                  Status = true,
                
             };
        }

        async Task<CommentsResponseModel> ICommentService.GetCommentsOfAPost(string PostId)
        {
            var postComments = await _CommentRepository.GetCommentsOfAPost(PostId);
           if(postComments == null)
           {    return new CommentsResponseModel
               {
                     Message = "Post Has No Comments yet",
                     Status = false,
               };
           }
            return new CommentsResponseModel
           { 
               Message = "Successful Retrieval",
               Status = true,
               Data =  postComments.Select(comment => new CommentDto
                {
                //  MemberId = comment.MemberId,
                //   MemberName = comment.Member.User.UserName,
                  CommentText = comment.CommentText,
                  CommentDate = comment.CommentDate,
                //   PostCreator = comment.Post.Member.User.UserName,
                  PostDate = comment.Post.DatePosted,
                //   PostId = comment.PostId, 
                  Id = comment.Id,
              }).ToList()
           };
        }
    }
}