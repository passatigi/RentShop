using System.Linq;
using System.Threading.Tasks;
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
    }
}