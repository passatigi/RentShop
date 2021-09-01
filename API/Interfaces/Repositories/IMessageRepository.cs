using System.Collections;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        Task<IEnumerable> GetMessageThread(
            int currentId,
            int recipientId,
            int orderId,
            int startFrom = 0
        );

        void AddGroup(Group group);
        void RemoveConnection(Connection connection);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);

        Task<Group> GetGroupForConnection(string connectionId);
    }
}