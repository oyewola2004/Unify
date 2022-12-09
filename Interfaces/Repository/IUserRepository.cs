using System.Threading.Tasks;
using UNIFY.Identity;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetById(string id);
         Task<Member> GetMemberByUserId(string UserId);
    }
}
