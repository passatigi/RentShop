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
        public async Task<ActionResult> AddOrder(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            order.Status = "order in processing";
            order.DeliverymanId = 1;


            var deliverymens = await _dataContext.DeliverymanSchedules.Where(p => p.StartDelivery.Date == order.OrderDate.Date).ToListAsync();
            if (deliverymens.Count == 0) return BadRequest("No delivery on start day");

            var deliverymensReturn = await _dataContext.DeliverymanSchedules.Where(p => p.StartDelivery.Date == order.OrderDate.Date).ToListAsync();
            if (deliverymensReturn.Count == 0) return BadRequest("No delivery on last day");
            
            var rand = new Random();
            order.DeliverymanId = deliverymens.ElementAt(rand.Next(0, deliverymens.Count - 1)).Id;
            order.DeliverymanReturnId = deliverymensReturn.ElementAt(rand.Next(0, deliverymensReturn.Count - 1)).Id;

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