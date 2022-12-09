using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Unify.UNIFY.Dtos;
using UNIFY.Interfaces.Services;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityAgencyController : ControllerBase
    {
        
        private readonly ISecurityService _securityService;
        private readonly ISecurityAgencyService _securityAgencyService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SecurityAgencyController(ISecurityService securityService, ISecurityAgencyService securityAgencyService, IWebHostEnvironment host)
        {
            _securityService = securityService;
            _securityAgencyService = securityAgencyService;
            _webHostEnvironment = host;
        }

         [HttpPost("RegisterSecurityAgency")]
        public async Task<IActionResult> RegisterSecurityAgency([FromForm] CreateSecurityAgencyRequestModel model) 
        {
            
            var registersecurityAgency = await _securityAgencyService.Create(model);
            if (registersecurityAgency.Status == false)
            {
                return BadRequest(registersecurityAgency);
            }
            return Ok(registersecurityAgency);
        }

         [HttpGet("GetSecurityAgencyById/{id}")]
        public async Task<IActionResult> GetSecurityAgencyById([FromRoute] string id)
        {
            var securities = await _securityAgencyService.Get(id);
            if (securities.Status)
            {
                return Ok(securities);
            }
            return BadRequest(securities);
        }
        [HttpGet("GetAllSecurityAgency")]
        public async Task<IActionResult> GetAllSecurities()
        {
            var securities = await _securityAgencyService.GetAll();
            return Ok(securities);
        }
    }
}