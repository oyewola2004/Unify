using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Interfaces.Services;
using UNIFY.Model.Enums;

namespace Unify.UNIFY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
         private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost("CreateMessage")]
         public async Task<IActionResult> CreateMessage(CreateMessageRequestModel model)
         {
             var response = await  _messageService.Create(model);
             if (!response.Status)
             {
                 return BadRequest(response);
             }
             return Ok(response);
         }

          [HttpGet("GetMessage/{Id}")]
         public async Task<IActionResult> GetMessage([FromRoute] string Id)
         {
               var response = await  _messageService.Get(Id);
              if (!response.Status)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
        //  [HttpGet("GetMessage")]
        //  public async Task<IActionResult> GetMessages()
        //  {
        //        var response = await  _messageService.Get();
        //       if (!response.Status)
        //        {
        //          return BadRequest(response);
        //         }
        //      return Ok(response);
        //  }

          [HttpGet("GetMessageByType")]
         public async Task<IActionResult> GetMessageByType(MessageType messageType)
         {
               var response = await  _messageService.GetMessageByType(messageType);
              if (!response.Status)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
         [HttpPut("UpdateMessage/{Id}")]
         public async Task<IActionResult> UpdateNote([FromBody] UpdateMessageRequestModel model, [FromRoute] string Id)
          {
             var response = await  _messageService.Update(model,Id);
             if (!response.Status)
             {
                 return BadRequest(response);
             }
             return Ok(response);
          }
    }
}