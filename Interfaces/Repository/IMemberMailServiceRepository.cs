using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;
using UNIFY.Interfaces.Repository;

namespace Interfaces.Repository
{
    public interface IMemberMailServiceRepository : IRepository<MemberMailService>
    {
        Task<MemberMailService> GetMemberMessage(string Id);
        Task<IEnumerable<MemberMailService>> GetAdminMessages();
    }
}