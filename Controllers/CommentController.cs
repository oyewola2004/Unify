using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Implementation.Services;
using Unify.UNIFY.Interfaces.Services;
using Unify.UNIFY.Model.Entities;

namespace Unify.UNIFY.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
         private readonly IPostService _postService;
        public CommentController(ICommentService commentService,IPostService postService , IWebHostEnvironment webHostEnvironment)
        {
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
            _postService = postService;
        }

         [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(CreateCommentRequestModel model, string PostId, string MemberId)
        {
            string Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var comment = await _commentService.CreateComment(model,MemberId,PostId);
            if(!comment.Status) return BadRequest(comment);
            return Ok(comment);
        }
        [HttpGet("GetCommentsOfAPost/{PostId}")]
        public async Task<IActionResult> GetCommentsOfAPost([FromRoute]  string PostId)
        {

            var comment = await _commentService.GetCommentsOfAPost(PostId);
            if(!comment.Status) return BadRequest(comment);
            return Ok(comment);
        }

         [HttpPut("UpdateAComment/{Id}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequestModel model,  string CommentId)
        {

            var comment = await _commentService.UpdateComment(model, CommentId);
            if(!comment.Status) return BadRequest(comment);
            return Ok(comment);
        }
         [HttpGet("GetApplicationComments")]
        public async Task<IActionResult> GetComments()
        {
            var comment = await _commentService.Get();
            if(!comment.Status) return BadRequest(comment);
            return Ok(comment);
        }
         [HttpGet("GetAnApplicationComment/{CommentId}")]
        public async Task<IActionResult> GetAComment([FromRoute]  string CommentId)
        {
            var comment = await _commentService.Get(CommentId);
            if(!comment.Status) return BadRequest(comment);
            return Ok(comment);
        }
    }
}