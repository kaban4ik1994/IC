using System.Linq;
using IC.Entities.Models;
using IC.Services.Interfaces;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace IC.Services
{
    public class MessageService : Service<Message>, IMessageService
    {
        public MessageService(IRepositoryAsync<Message> repository)
            : base(repository)
        {
        }

        public long GetNewMessagesCountByUserId(long userId)
        {
            return Query(message => message.ToUserId == userId && !message.IsViewed && message.ShowForSecondUser).Select().Count();
        }
    }
}
