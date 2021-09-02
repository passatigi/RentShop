using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
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

            _dataContext.Orders.Add(order);

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(Order order)
        {
            _dataContext.Entry(order).State = EntityState.Modified;

            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
