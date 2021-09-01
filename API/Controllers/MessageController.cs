using System.Linq;
using API.Data;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly DataContext _dataContext;

        public MessageController(UnitOfWork unitOfWork, IMapper mapper, DataContext dataContext)
        {
            _dataContext = dataContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public void GetMessageThreadsList()
        {
            var userId = User.GetUserId();

            _dataContext.Messages.Include(m => m.Recipient)
                .Where(m => m.RecipientId == userId || m.SenderId == userId)
                .GroupBy(m => m.Order)
                .Select(m => 
                    new { 
                        OrderId = m.OrderId, 
                        Content = m.Content, 
                        СompanionName = m.Recipient.FullName, 
                        Date = m.MessageSent
                    }).;
                    
        }
    }
}