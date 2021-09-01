using System.Collections;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        Task<IEnumerable> GetMessageThread(string currentUserName, string recipientUserName, int orderId, int startFrom = 0);
    }
}