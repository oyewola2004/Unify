using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UNIFY.Dtos;
using UNIFY.Interfaces.Services;

namespace UNIFY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

         [HttpPost("CreateCategory")]
         public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequestModel model)
         {
             var category = await _categoryService.Create(model);
            if (category.Status == false)
            {
                return BadRequest(category);
            }
            return Ok(category);
         }
         [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryRequestModel model, [FromRoute] string id) 
        {
            var categories = await _categoryService.Update(model, id);
            if (!categories.Status)
            {
                return BadRequest(categories);
            }
            return Ok(categories);
        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string id)
        {
            var category = await _categoryService.Get(id);
            if (category.Status)
            {
                return Ok(category);
            }
            return BadRequest(category);
        }
         [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllMarketPlace()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
    }
}