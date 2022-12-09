using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unify.UNIFY.Interfaces.Repository;
using UNIFY.Context;
using UNIFY.Implementation.Repository;
using UNIFY.Model.Entities;
using UNIFY.Model.Enums;

namespace Unify.UNIFY.Implementation.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
         public MessageRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Message> GetMessageByType(MessageType messageType)
        {
            var message =  await _context.Messages
            .SingleOrDefaultAsync( L => L.MessageType== messageType);
            return message;
        }
    }
}