using System.Threading.Tasks;
using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DeliverymanController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public DeliverymanController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;

        }

        // [HttpPost("shedule-day")]
        // public async Task<ActionResult> AddSheduleDay()
        // {

        // }
    }
}