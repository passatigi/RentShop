using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<OrderDto>> GetOrderList(int userId, DateTime date)
        {
            return await _context.Orders.Where(
                o => o.DeliverymanId == userId &&
                o.RequiredDate.Date == date.Date ||
                o.RequiredReturnDate.Date == date.Date)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.RealProduct)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId){
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}