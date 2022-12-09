using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetById(string id);
    }
}