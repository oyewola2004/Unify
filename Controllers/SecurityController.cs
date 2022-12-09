using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using UNIFY.Interfaces.Services;
using static UNIFY.Dtos.SecurityDto;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly ISecurityService _securityService;
        private readonly ISecurityAgencyService _securityAgencyService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SecurityController(ISecurityService securityService, ISecurityAgencyService securityAgencyService, IWebHostEnvironment host)
        {
            _securityService = securityService;
            _securityAgencyService = securityAgencyService;
            _webHostEnvironment = host;
        }
        

         [HttpPost("RegisterSecurity")]
        public async Task<IActionResult> RegisterSecurity([FromForm] SecurityRequestModel model) 
        {
            
            var registersecurity = await _securityService.RegisterSecurity(model);
            if (registersecurity.Status == false)
            {
                return BadRequest(registersecurity);
            }
            return Ok(registersecurity);
        }

         [HttpPut("UpdateSecurity/{id}")]
        public async Task<IActionResult> UpdateSecurity([FromForm] UpdateSecurityRequestModel model, [FromRoute] string id) 
        {
            var security = await _securityService.UpdateSecurity(model, id);
            if (!security.Status)
            {
                return BadRequest(security);
            }
            return Ok(security);
        }


         [HttpGet("GetSecurityById/{id}")]
        public async Task<IActionResult> GetSecurityById([FromRoute] string id)
        {
            var securities = await _securityService.GetSecurityById(id);
            if (securities.Status)
            {
                return Ok(securities);
            }
            return BadRequest(securities);
        }
        [HttpGet("GetAllSecurities")]
        public async Task<IActionResult> GetAllSecurities()
        {
            var securities = await _securityService.GetAllSecurities();
            return Ok(securities);
        }
    }
}