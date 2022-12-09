using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Dtos;

namespace UNIFY.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponseModel> Create(CreateCategoryRequestModel model);
        Task<CategoryResponseModel> Update(UpdateCategoryRequestModel model, string Id);
        Task<CategoryResponseModel> Get(string Id);
        Task<CategoriesResponseModel> GetAll();
        Task<bool> Delete(string Id);
    }
}