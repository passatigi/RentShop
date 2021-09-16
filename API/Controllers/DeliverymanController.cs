using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DeliverymanController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeliverymanController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpPut("schedule-day")]
        public async Task<ActionResult> EditScheduleDay(DeliverymanScheduleDto deliverymanSheduleDto)
        {
            var userId = User.GetUserId();
            var schedule =  await _unitOfWork.DeliveryManRepository.GetDayScheduleAsync(
                userId, deliverymanSheduleDto.StartDelivery);
            
            if(schedule == null)
            {
                schedule = new DeliverymanSchedule { 
                    DeliverymanId = userId };
            }

            schedule.StartDelivery = deliverymanSheduleDto.StartDelivery;
            schedule.EndDelivery = deliverymanSheduleDto.EndDelivery;

            if(schedule.Id == 0){
                _unitOfWork.DeliveryManRepository.AddSchedule(schedule);
            }
            
            if (await _unitOfWork.Complete()) return Ok(schedule);

            return BadRequest("Failed to edit schedule");
        }

        [HttpGet("schedule-month")]
        public async Task<ActionResult<IEnumerable<DeliverymanScheduleDto>>> GetMonthShedule
            ([FromQuery] short year, [FromQuery] short month)
        {
            var userId = User.GetUserId();
            var date = DateTime.Parse($"1/{month}/{year}");

            var schedule = await _unitOfWork.DeliveryManRepository.GetMonthScheduleAsync(userId, date);

            return Ok(schedule);
        }


        [HttpGet("delivery-list")]
        public async Task<ActionResult<IEnumerable<Order>>> GetDeliveryList(DateTime date)
        {
            var userId = User.GetUserId();
            var orders = await _unitOfWork.OrderRepository.GetOrderList(userId, date);

            return Ok(orders);
        }

        [HttpPut("delivery-list")]
        public async Task<ActionResult> UpdateOrderStatus(UpdateOrderStatusDto updateOrderStatusDto)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(updateOrderStatusDto.OrderId);

            if (order == null) return NotFound("Order not found");

            order.Status = updateOrderStatusDto.NewStatus;
            if (order.Status == "Delivered")
                order.ShippedDate = DateTime.UtcNow;
            else if (order.Status == "Returned")
                order.ReturnDate = DateTime.UtcNow;

            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest("Failed to update order status");
        }

    }
}