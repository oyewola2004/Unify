using System;
using System.IO;
using System.Threading.Tasks;
using Dtos;
using Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Model.Enums;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketPlaceController : ControllerBase
    {
        private readonly IMarketPlaceService _marketPlaceService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MarketPlaceController(IMarketPlaceService marketPlaceService, IWebHostEnvironment webHostEnvironment)
        {
            _marketPlaceService = marketPlaceService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("CreateMarketPlace")]
         public async Task<IActionResult> RegisterMarketPlace([FromForm] CreateMarketPlaceRequestModel model) 
        {
             var files = HttpContext.Request.Form;

            if (files != null && files.Count > 0)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo info = new FileInfo(file.FileName);
                    string image = Guid.NewGuid().ToString() + info.Extension;
                    string path = Path.Combine(imageDirectory, image);
                    using(var filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    model.Image = (image);
                }
            }
            var registermarketplace = await _marketPlaceService.CreateMarketPlace(model);
            if (registermarketplace.Status == false)
            {
                return BadRequest(registermarketplace);
            }
            return Ok(registermarketplace);
        }

        [HttpPut("UpdateMarketPlace")]
        public async Task<IActionResult> UpdateMarketPlace([FromForm] UpdateMarketPlaceRequestModel model, [FromRoute] string id) 
        {
            var marketplace = await _marketPlaceService.UpdateMarketPlace(model, id);
            if (!marketplace.Status)
            {
                return BadRequest(marketplace);
            }
            return Ok(marketplace);
        }

        [HttpGet("GetMarketPlaceById/{id}")]
        public async Task<IActionResult> GetMarketPlaceById([FromRoute] string id)
        {
            var marketPlace = await _marketPlaceService.GetMarketById(id);
            if (marketPlace.Status)
            {
                return Ok(marketPlace);
            }
            return BadRequest(marketPlace);
        }
        [HttpGet("GetAllMarketPlaces")]
        public async Task<IActionResult> GetAllMarketPlace()
        {
            var marketPlaces = await _marketPlaceService.GetAllMarketPlace();
            return Ok(marketPlaces);
        }

          [HttpGet("GetMarketPlaceByType")]
         public async Task<IActionResult> GetMarketPlaceByType(int Type)
         {
               var response = await  _marketPlaceService.GetMarketPlaceByType(Type);
              if (!response.Status)
               {
                 return BadRequest(response);
                }
             return Ok(response);
         }
    }
}