using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _dataContext;

        public MessageController(IUnitOfWork unitOfWork, IMapper mapper, DataContext dataContext)
        {
            _dataContext = dataContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetMessageThreadsList()
        {
            var userId = User.GetUserId();


            var messages = await _dataContext.Messages
                .FromSqlRaw(
                "SELECT * from [dbo].[Messages] " + 
                "where Id = any (SELECT MAX(Id) FROM [dbo].[Messages] " + 
                $" Where [SenderId] = {userId} or [RecipientId] = {userId} GROUP by [OrderId] ) ")
                .ToListAsync();

            return Ok(messages);    
        }
        [HttpGet("thread-info")]
        public async Task<ActionResult> GetMessageThreadInfo(int recipientId, int orderId)
        {
            
            var userId = User.GetUserId();

            var order = await _dataContext.Orders
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == orderId);
            
            if(order == null) return NotFound("Cannot find order");

            if(order.CustomerId == userId || order.DeliverymanId == userId || User.IsInRole("Admin")) {
                var recipient = await _dataContext.Users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(u => u.Id == recipientId);
                if(recipient == null) return NotFound("Cannot find user");
                return Ok(new {Recipient = recipient, Order = order});
            }
            else
            {
                return BadRequest("ne nado");
            }
        }
    }
}