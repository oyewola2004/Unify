using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Model.Entities;

namespace UNIFY.Dtos
{
    public class CategoryDto
    {
        public string Id {get; set;}
        public string CategoryName {get; set;}
        public IList<PostDto>  posts{get; set;}
    }
     public class CreateCategoryRequestModel
     {
        public string CategoryName {get; set;}
     }
      public class UpdateCategoryRequestModel
     {
        public string CategoryName {get; set;}
     }

      public class CategoryResponseModel : BaseResponse
    {
        public CategoryDto Data { get; set; }
    }
    public class CategoriesResponseModel : BaseResponse
    {
        public ICollection<CategoryDto> Data { get; set; } = new HashSet<CategoryDto>();
    }
}