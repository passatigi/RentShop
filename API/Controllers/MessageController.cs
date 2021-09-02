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

        [HttpGet]
        public async Task<ActionResult> GetMessageThreadsList()
        {
            var userId = User.GetUserId();

            var messages = await _dataContext.Messages.Include(m => m.Recipient)
                .Where(m => m.RecipientId == userId || m.SenderId == userId)
                .OrderBy(m => m.Id)
                .GroupBy(m => m.Order)
                .Select(m => m.TakeLast(1))
                .ToListAsync();


            return Ok(messages);
                    
        }
    }
}