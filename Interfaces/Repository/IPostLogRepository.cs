using System.Collections.Generic;
using System.Threading.Tasks;
using Unify.UNIFY.Model.Entities;
using UNIFY.Interfaces.Repository;

namespace Unify.UNIFY.Interfaces.Repository
{
    public interface IPostLogRepository : IRepository<PostLog>
    {
         Task<IEnumerable<PostLog>> GetLogsOfPostCreatedToday();
    }
}