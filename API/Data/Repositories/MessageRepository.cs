using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public static int PageSize { get; set; } = 10;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public void AddMessage(Message message)
        {
            _dataContext.Messages.Add(message);
        }

        public async Task<IEnumerable> GetMessageThread(
            int currentId,
            int recipientId,
            int orderId,
            int startFrom = 0)
        {
            var messages = _dataContext.Messages
            .Where(
                m => m.OrderId == orderId &&
                m.RecipientId == currentId
                && m.SenderId == recipientId
                || m.RecipientId == recipientId
                && m.SenderId == currentId
            ).AsQueryable();

            var unreadMessages = messages.Where(m => m.isRead == false && m.RecipientId == currentId)
                .AsQueryable();



            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.isRead = true;
                }
            }

            if (startFrom != 0)
            {
                messages = messages.Where(m => m.Id < startFrom);
            }

            return await messages
                .OrderByDescending(m => m.MessageSent)
                .Take(PageSize)
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void AddGroup(Group group)
        {
            _dataContext.Groups.Add(group);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _dataContext.Connections.FindAsync(connectionId);
        }
        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _dataContext.Groups.Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public void RemoveConnection(Connection connection)
        {
            _dataContext.Connections.Remove(connection);
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _dataContext.Groups
                        .Include(c => c.Connections)
                        .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                        .FirstOrDefaultAsync();
        }
    }
}