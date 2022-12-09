using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UNIFY.Context;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
         public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Category> GetById(string id)
        {
             var category = await _context.Categories.Include(x => x.Post)
            .Where(x => x.IsDeleted == false).SingleOrDefaultAsync(x => x.Id == id);
            return category;
        }
    }
}