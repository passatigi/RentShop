using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OrderController(DataContext dataContext, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(p => p.OrderProducts)
                .ThenInclude(s => s.RealProduct)
                .Where(c => c.Id == id)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpGet("list/{userid}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserId(int userid)
        {
            var orders = await _context.Orders
                .Include(p => p.OrderProducts)
                .ThenInclude(s => s.RealProduct)
                .Where(c => c.CustomerId == userid)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (orders == null) return NotFound();

            return Ok(orders);
        }

        [HttpPost("new")]
        public async Task<ActionResult> AddOrder(OrderDto orderDto)
        {
            orderDto.OrderDate = DateTime.UtcNow;
            orderDto.Status = "Avaiting delivery";
            orderDto.DeliverymanId = 5;


            var deliverymen = await _dataContext.DeliverymanSchedules.Where(p => p.StartDelivery.Date == orderDto.RequiredDate.Date).ToListAsync();
            if (deliverymen.Count == 0) return BadRequest("No delivery on start day");

            var deliverymenReturn = await _dataContext.DeliverymanSchedules.Where(p => p.StartDelivery.Date == orderDto.RequiredReturnDate.Date).ToListAsync();
            if (deliverymenReturn.Count == 0) return BadRequest("No delivery on last day");
            
            var rand = new Random();
            orderDto.DeliverymanId = deliverymen.ElementAt(rand.Next(0, deliverymen.Count - 1)).Id;
            orderDto.DeliverymanReturnId = deliverymenReturn.ElementAt(rand.Next(0, deliverymenReturn.Count - 1)).Id;

            var order =  _mapper.Map<Order>(orderDto);
            _dataContext.Orders.Add(order);

            if (await _dataContext.SaveChangesAsync() > 0) return Ok();

            return BadRequest("An error occurred while adding an order. Try this later");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(Order order)
        {
            _dataContext.Entry(order).State = EntityState.Modified;

            if (await _dataContext.SaveChangesAsync() > 0) return Ok();

            return BadRequest("An error occurred while updating the order. Try this later");
        }
    }
}