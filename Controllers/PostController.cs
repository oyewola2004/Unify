using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Interfaces.Services;

namespace Unify.UNIFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
         private readonly IPostService _postService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(IPostService postService , IWebHostEnvironment webHostEnvironment)
        {
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
        }

         [HttpPost("CreatePost/{UserId}")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequestModel model, [FromRoute] string UserId)
        {
            var files = HttpContext.Request.Form;
                if(files.Count != 0)
                {
                    string VideoDirectory = Path.Combine(_webHostEnvironment.WebRootPath,"VideoPostsFiles");
                     Directory.CreateDirectory(VideoDirectory);
                     foreach (var file in files.Files)
                     {
                          FileInfo fileInfo= new FileInfo(file.FileName);
                          string videoFile = "post" +  Guid.NewGuid().ToString().Substring(0,7) + $"{fileInfo.Extension}";
                          string fullPath= Path.Combine(VideoDirectory,videoFile);
                          using(var fileStream= new FileStream(fullPath,FileMode.Create))
                          {
                              file.CopyTo(fileStream);
                          }
                          model.VideoFile = videoFile;
                     }
                }
           var post = await _postService.Create(model,UserId);
           if(!post.Status) return BadRequest(post);
           
           return Ok(post);
        }

         [HttpGet("GetPost/{Id}")]
        public async Task<IActionResult> GetPost([FromRoute] string Id)
        {
           var post = await _postService.Get(Id);
           if(!post.Status) return BadRequest(post);
           return Ok(post);
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
           var post = await _postService.Get();
           if(!post.Status) return BadRequest();
           return Ok(post);
        }

         [HttpPut("UpdatePost/{Id}")]
        public async Task<IActionResult> UpdatePost(UpdatePostRequestModel model, string id)
        {
           var post = await _postService.Update(model,id);
           if(!post.Status) return BadRequest();
           return Ok(post);
        }
        [HttpGet("GetPostOfLoggedInUser")]
        public async Task<IActionResult> GetPostOfLoggedInUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var post = await _postService.GetPostsOfUser(id);
           if(!post.Status) return BadRequest();
           return Ok(post);
        }

        [HttpDelete("DeletePostOfLoggedInUser/{Id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
          var deletion = await _postService.Delete(id);
          if(!deletion) return BadRequest(deletion);
          return Ok(deletion);
        }
         [HttpGet("GetPostByUser/{UserId}")]
        public async Task<IActionResult> GetPostByUser(string userId)
        {
          var posts = await _postService.GetPostsOfUser(userId);
          if(posts.Status) return BadRequest(posts);
          return Ok(posts);
        }
        [HttpPut("ReportPost/{Id}/{UserId}")]
        public async Task<IActionResult> ReportPost ([FromRoute] string id , string UserId)
        {
            var post = await _postService.ReportPost(id , UserId);
            if (post != null)
            {
                return Ok(post);
            }
            return BadRequest(post);
        }
         [HttpGet("GetNotVerifiedReportedPosts")]
        public async Task<IActionResult> GetNotVerifiedReportedPosts()
        {
            var posts = await _postService.GetNotVerifiedReportedPosts();
            return Ok(posts);
        }
         [HttpGet("GetAllReportedPosts")]
        public async Task<IActionResult> GetAllReportedPosts()
        {
            var posts = await _postService.GetAllReportedPost();
            return Ok(posts);
        }

         [HttpGet("GetPostsForToday")]
        public async Task<IActionResult> GetPostsForToday()
        {
            var posts = await _postService.GetPostsForToday();
            return Ok(posts);
        }

         [HttpGet("GetAllPostByCategory/{CategoryId}")]
        public async Task<IActionResult> GetAllPostByCategory([FromRoute] string CategoryId)
        {
            var post = await _postService.GetAllPostByCategory(CategoryId);
            if (post.Status)
            {
                return Ok(post);
            }
            return BadRequest(post);
        }
    }


}