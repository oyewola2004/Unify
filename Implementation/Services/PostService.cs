using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Interfaces.Services;
using Unify.UNIFY.Model.Entities;
using UNIFY.Dtos;
using UNIFY.Interfaces.Repository;

namespace Unify.UNIFY.Implementation.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostService(IPostRepository postRepository,ICategoryRepository categoryRepository, IMemberRepository memberRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _postRepository = postRepository;
            _memberRepository = memberRepository;
            _categoryRepository = categoryRepository;
            _UserRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PostResponseModel> Create(CreatePostRequestModel model, string UserId)
        {   
            var userInPost = await _UserRepository.GetMemberByUserId(UserId);
            if(userInPost == null)
            {
                return new PostResponseModel
                {
                    Message = "Id not found",
                    Status = false
                };
            }
            var category = await _categoryRepository.GetById(model.CategoryId);
            System.Console.WriteLine(category);
            if(category == null)
            {
                 return new PostResponseModel
                {
                    Message = "Category not found",
                    Status = false
                };
            }

            var post = new Post
            {
                PostContent = model.PostContent,
                DatePosted = DateTime.UtcNow,
                VideoFile = model.VideoFile,
                CategoryId = model.CategoryId,
                Title = model.Title,
                MemberId = userInPost.Id,
                Member = userInPost
            };

            await _postRepository.Register(post);
            return new PostResponseModel
            {
                Message = "Post Created",
                Status = true,
               Data = new PostDto
               {
                
                PostId = post.Id,
                PostContent = post.PostContent,
                Title = post.Title,
                VideoFile = post.VideoFile,
                CategoryName=post.Category.Name,
              
               }
            };
        }

        public async Task<bool> Delete(string Id)
        {
            var post = await _postRepository.GetMemberPost(Id);
           if(post==null) return false;
           post.IsDeleted = true;
           await _postRepository.Delete(post);
           return true;
        }

        public async Task<PostResponseModel> Get(string Id)
        {
            var appUserPost = await _postRepository.GetPostById(Id);
            // var user = await _UserRepository.GetById(UserId);
            if(appUserPost == null)
            {
                 return new PostResponseModel
                {
                    Message = "Id not found",
                    Status = false
                };
            }

            var appUserPostReturned =  new PostDto
            {
                PostId = appUserPost.Id,
                // UserName = user.UserName,
                // Email = user.Email,
                PostContent = appUserPost.PostContent,
                DatePosted = appUserPost.DatePosted,
                Title = appUserPost.Title,
                VideoFile =  appUserPost.VideoFile  ?? String.Empty,
                Comments = appUserPost.Comments.Select( L=> new CommentDto
                 {
                //    MemberId = L.MemberId,
                //    MemberName = L.Member.User.UserName,
                   CommentText = L.CommentText,
                //    PostId = L.PostId,
                   CommentDate = L.CommentDate,
                //    PostCreator = L.Post.Member.User.UserName,
                   PostDate =L.Post.DatePosted,
                   Id = L.Id,
                  }
                ).ToList(),

            };

            return new PostResponseModel
           {
               Data = appUserPostReturned,
                Message = "Post Retrieval Successful",
                Status = true,
               
           };
        }

        public async Task<PostsResponseModel> Get()
        {
            var userPost = await _postRepository.GetAllPost();
           var userPostsReturned = userPost.Select(appUserPost=> new PostDto
           {
               UserId = appUserPost.UserId,
                PostId = appUserPost.Id,
                UserName = appUserPost.Member.User.UserName,
                Email = appUserPost.Member.User.Email,
                PostContent = appUserPost.PostContent,
                VideoFile = appUserPost.VideoFile,
                DatePosted = appUserPost.DatePosted,
                CreatorPhoto = appUserPost.Member.Image,
                Title = appUserPost.Title,
                

                Comments = appUserPost.Comments.Select( L=> new CommentDto
                 {
                   MemberId = L.MemberId,
                   MemberName = L.Member.User.UserName,
                   CommentText = L.CommentText,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                   PostCreator = L.Post.Member.User.UserName,
                   PostDate =L.Post.DatePosted,
                   Id = L.Id,
                  }
                ).ToList(),   
           }).ToList();
           return new PostsResponseModel
           {
               Data = userPostsReturned,
                Message = "User Retrieval Successful",
                Status = true,
           };
        }

        public async Task<CategoriesResponseModel> GetAllPostByCategory(string CategoryId)
        {
            var member = await _postRepository.GetAllPost();
             var post = await _postRepository.GetAllPostByCategory(CategoryId);
              if (post == null)
            {
                return new CategoriesResponseModel
                {
                    Message = "Post not found",
                    Status = true
                };
            }
             var communityDto = post.Select(category => new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.Category.Name,
                posts=category.Category.Post.Select(g=>new PostDto{
                    PostContent = g.PostContent,
                    Title = g.Title,
                    DatePosted = g.DatePosted,
                    VideoFile = g.VideoFile
                }).ToList(),

            }).ToList();
             return new CategoriesResponseModel
            {
                Data = communityDto,
                Message = "All Post Under this Category",
                Status = true
            };


        }

        public async Task<PostsResponseModel> GetAllReportedPost()
        {
            var posts = await _postRepository.GetAllReportedPost();
            if(posts == null)
            {
                return new PostsResponseModel
                {
                    Message = "No post found",
                    Status = false
                };
            }
             var postDtos = posts.Select(x => new PostDto
            {
                Title = x.Title,
                PostContent = x.PostContent,
                VideoFile = x.VideoFile,
                //UserName = x.User.UserName,
                //PostId = x.Id,
                //UserId = x.UserId,
                //Email = x.User.Email

                
            }).ToList();

            return new PostsResponseModel
            {
                Data = postDtos,
                Message = "List of all reported post",
                Status = true
            };
        }

        public async Task<PostsResponseModel> GetNotVerifiedReportedPosts()
        {
             var posts = await _postRepository.GetNotVerifiedReportedPosts();
            var postDtos = posts.Select(x => new PostDto
            {
                PostContent = x.PostContent,
                Title = x.Title,
                VideoFile = x.VideoFile
                
            }).ToList();

            return new PostsResponseModel
            {
                Data = postDtos,
                Message = "List of all not verified post",
                Status = true
            };
        }

        public async Task<PostsResponseModel> GetPostsOfUser(string UserId)
        {
             var userinPost = await _UserRepository.Get(x => x.Id == UserId);
            //var userId = userinPost.Member.User.Id;
             var postsOfUser = await _postRepository.GetAllPosts(L=> L.MemberId == UserId);
             if(postsOfUser == null) return new PostsResponseModel
          {
           Message = "Post Not Found",
           Status = false,
          };

            return new  PostsResponseModel
           {
               Message = "posts retrieved Successfully",
               Status =true,
               Data =  postsOfUser.Select(post => new PostDto
               {
                    UserId = post.UserId,
                    PostId = post.Id,
                    UserName = post.Member.User.UserName,
                    Email = post.Member.User.Email,
                    PostContent = post.PostContent,
                    VideoFile = post.VideoFile,
                    DatePosted = post.DatePosted,
                    CreatorPhoto = post.Member.Image,
                    Title = post.Title,
                    Comments = post.Comments.Select( L=> new CommentDto
                    {
                    MemberId = L.MemberId,
                    MemberName = L.Member.User.UserName,
                    CommentText = L.CommentText,
                    PostId = L.PostId,
                    CommentDate = L.CommentDate,
                    PostCreator = L.Post.Member.User.UserName,
                    PostDate =L.Post.DatePosted,
                    Id = L.Id,
                    }
                ).ToList(),   
               }).ToList()
           };

        }

        public async Task<BaseResponse> ReportPost(string id , string UserId)
        {
            var user = await _UserRepository.GetById(UserId);
            if(user == null)
            {
                return new BaseResponse
                {
                    Message = "User Not Found",
                    Status = false
                };
            }
            var post = await _postRepository.GetPostById(id);
            post.ISReported = true;
            var updatepost = await _postRepository.Update(post);
            if(updatepost == null)
            {
                return new BaseResponse
                {
                    Message = "Post not found",
                    Status = false
                };
            }
             return new BaseResponse
            {
                Message = "Post Reported successfully,Wait for the admins Inspection",
                Status = true
            };
        }

        public async Task<PostResponseModel> Update(UpdatePostRequestModel model, string Id)
        {
           var appUserPost= await _postRepository.GetMemberPost(Id);
          if(appUserPost == null) return new PostResponseModel
          {
           Message = "Post Not Found",
           Status = false,
          };
          appUserPost.PostContent = model.PostContent;
          appUserPost.Title = model.PostTitle;
          await _postRepository.Update(appUserPost);

          return new PostResponseModel
           {
               Message = "Update Successful",
               Status =true,
               Data = new PostDto
               {
                    UserId = appUserPost.UserId,
                    PostId = appUserPost.Id,
                    UserName = appUserPost.Member.User.UserName,
                    Email = appUserPost.Member.User.Email,
                   PostContent = appUserPost.PostContent,
                   VideoFile = appUserPost.VideoFile,
                    DatePosted = appUserPost.DatePosted,
                   Title = appUserPost.Title,
                  Comments = appUserPost.Comments.Select( L=> new CommentDto
                 {
                   MemberId = L.MemberId,
                   MemberName = L.Member.User.UserName,
                   CommentText = L.CommentText,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                   PostCreator = L.Post.Member.User.UserName,
                   PostDate =L.Post.DatePosted,
                   Id = L.Id,
                  }
                ).ToList(),   
               }
           };
        }

        async Task<PostsResponseModel> IPostService.GetPostsForToday()
        {
             var postsToday = await _postRepository.GetAllPostsToday();
             System.Console.WriteLine(postsToday);
            if(postsToday.Count() == 0) return new PostsResponseModel
            {
            Message = "No Post Found",
            Status = false,
            };
           return new  PostsResponseModel
           {
               Message = "Today's posts retrieved Successfully",
               Status = true,
               Data =  postsToday.Select(post => new PostDto
               {
                UserId = post.MemberId,
                PostId = post.Id,
                UserName = post.Member.User.UserName,
                Email = post.Member.User.Email,
                PostContent = post.PostContent,
                VideoFile = post.VideoFile,
                DatePosted = post.DatePosted,
                Title = post.Title,
                  Comments = post.Comments.Select( L=> new CommentDto
                 {
                //    MemberId = L.MemberId,
                //    MemberName = L.Member.User.UserName,
                   CommentText = L.CommentText,
                   PostId = L.PostId,
                   CommentDate = L.CommentDate,
                //    PostCreator = L.Post.Member.User.UserName,
                   PostDate =L.Post.DatePosted,
                   Id = L.Id,
                  }
                ).ToList(),   
               }).ToList(),
           };
           
        }
    }
}