using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unify.UNIFY.Interfaces.Repository;
using UNIFY.Dtos;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICommentRepository _CommentRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _PostRepository;

        public CategoryService(ICommentRepository CommentRepository, ICategoryRepository categoryRepository,IPostRepository PostRepository)
        {
            _CommentRepository = CommentRepository;
            _categoryRepository = categoryRepository;
            _PostRepository = PostRepository;
        }
        public async Task<CategoryResponseModel> Create(CreateCategoryRequestModel model)
        {
             var categoryExist = await _categoryRepository.Get(a => a.Name == model.CategoryName);
            if (categoryExist != null) return new CategoryResponseModel
            {
                Message = "Category already exist",
                Status = false,
            };
              var category = new Category
            {
                Name = model.CategoryName,
               

            };
             var categoryDto = new CategoryDto
             {
                  Id = category.Id,
                    CategoryName = category.Name,
                   
             };

            await _categoryRepository.Register(category);

            return new CategoryResponseModel
            {
                Data = categoryDto,
                Message = "Category created successfully",
                Status = true,
            };
        }

        public async Task<bool> Delete(string Id)
        {
            var category = await _categoryRepository.Get(x => x.Id ==Id);
           if(category==null) return false;
           category.IsDeleted = true;
           await _categoryRepository.Delete(category);
           return true;
        }

        public async Task<CategoryResponseModel> Get(string Id)
        {
             var category = await _categoryRepository.Get(a => a.Id == Id);
            if (category == null) return new CategoryResponseModel
            {
                Message = "Category not found",
                Status = false,
            };
             var categoryDto = new CategoryDto
             {
                  Id = category.Id,
                    CategoryName = category.Name,
                   
             };
            return new CategoryResponseModel
            {
                Data = categoryDto,
                Message = "Successful",
                Status = true,
              
            };
        }

        public async Task<CategoriesResponseModel> GetAll()
        {
              var categoriess = await _categoryRepository.GetAll();
            if (categoriess == null) return new CategoriesResponseModel
            {
                Message = "Category not found",
                Status = false,
            };
            var categoryDto = categoriess.Select(category=> new CategoryDto
            {
                Id =  category.Id,
                 CategoryName = category.Name,
            }).ToList();
            
            return new CategoriesResponseModel
            {
                Data = categoryDto,
                Message = "Successful",
                Status = true,
              
            };
        }

        public async Task<CategoryResponseModel> Update(UpdateCategoryRequestModel model, string Id)
        {
             var category = await _categoryRepository.Get(x => x.Id == Id);

            if (category == null) return new CategoryResponseModel
            {
                Message = "Category not found",
                Status = false,
            };
             category.Name = model.CategoryName;
            

            await _categoryRepository.Update(category);
            return new CategoryResponseModel
            {
                Message = "Updated Succesfully",
                Status = true,
                
            };
        }
    }
}