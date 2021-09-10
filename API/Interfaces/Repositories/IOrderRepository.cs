using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetOrderList(int userId, DateTime date);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}