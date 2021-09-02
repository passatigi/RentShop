using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut("schedule-day")]
        public async Task<ActionResult> EditScheduleDay(DeliverymanScheduleDto deliverymanSheduleDto)
        {
            var userId = User.GetUserId();
            var schedule = await _dataContext.DeliverymanSchedules
                            .FirstOrDefaultAsync(
                                s => s.DeliverymanId == userId &&
                                s.StartDelivery.Date == deliverymanSheduleDto.StartDelivery.Date
                                );
            
            if(schedule == null)
            {
                schedule = new DeliverymanSchedule { 
                    DeliverymanId = userId };
            }

            schedule.StartDelivery = deliverymanSheduleDto.StartDelivery;
            schedule.EndDelivery = deliverymanSheduleDto.EndDelivery;

            if(schedule.Id == 0){
                _dataContext.Add(schedule);
            }
            
            await _dataContext.SaveChangesAsync();

            return Ok(schedule);
        }

        [HttpGet("schedule-month")]
        public async Task<ActionResult<IEnumarable<DeliverymanScheduleDto>>> GetMonthShedule
                                                                                ([FromQuery] short year,
                                                                                [FromQuery] short month)
        {
            var userId = User.GetUserId();
            var date = DateTime.Parse($"1/{month}/{year}");

            var schedule = await _dataContext.DeliverymanSchedules
                .Where(s => s.DeliverymanId == userId && s.StartDelivery.Month == date.Month)
                .ProjectTo<DeliverymanScheduleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            return Ok(schedule);
        }

        
        [HttpGet("delivery-list")]
        public async Task<ActionResult<IEnumarable<Order>>> GetDeliveryList(DateTime date)
        {
            var userId = User.GetUserId();
            var orders = await _dataContext.Orders.Where(
                o => o.DeliverymanId == userId &&
                o.RequiredDate.Date == date.Date || 
                o.RequiredReturnDate.Date == date.Date)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.RealProduct)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpPut("delivery-list")]
        public async Task<ActionResult> UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatusDto)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.Id == updateOrderStatusDto.OrderId);

            if(order == null) return NotFound();

            order.Status = updateOrderStatusDto.NewStatus;
            if(order.Status == "Delivered")
                order.ShippedDate = DateTime.UtcNow;
            else if(order.Status == "Returned")
                order.ReturnDate = DateTime.UtcNow;

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

    }
}