using System;
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
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable> GetMessageThread(
            string currentEmail,
            string recipientEmail,
            int orderId,
            int startFrom = 0)
        {
            var messages = _dataContext.Messages
            .Where(
                m => m.OrderId == orderId &&
                m.Recipient.Email == currentEmail
                && m.Sender.Email == recipientEmail
                || m.Recipient.Email == recipientEmail
                && m.Sender.Email == currentEmail
            ).AsQueryable();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.Recipient.Email == currentEmail)
            .AsQueryable();



            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
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
    }
}