using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<DeliverymanSchedule> GetDayScheduleAsync(int userId, 
            DateTime startDeliveryDate);

        void EditSchedule(DeliverymanSchedule schedule);
        void AddSchedule(DeliverymanSchedule schedule);
        Task<List<DeliverymanScheduleDto>> GetMonthScheduleAsync(int userId,
            DateTime date);
        
    }
}