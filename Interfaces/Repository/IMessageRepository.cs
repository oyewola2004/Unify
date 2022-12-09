using System.Threading.Tasks;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;
using UNIFY.Model.Enums;

namespace Unify.UNIFY.Interfaces.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message> GetMessageByType(MessageType messageType);
    }
}