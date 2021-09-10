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
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeliveryManRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DeliverymanSchedule> GetDayScheduleAsync(int userId,
            DateTime startDeliveryDate)
        {
            return await _context.DeliverymanSchedules
                .FirstOrDefaultAsync(
                    s => s.DeliverymanId == userId &&
                    s.StartDelivery.Date == startDeliveryDate.Date
            );
        }

        public void EditSchedule(DeliverymanSchedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
        }

        public void AddSchedule(DeliverymanSchedule schedule)
        {
            _context.Add(schedule);
        }

        public async Task<List<DeliverymanScheduleDto>> GetMonthScheduleAsync(int userId,
            DateTime date)
        {
            var schedule = await _context.DeliverymanSchedules
                .Where(s => s.DeliverymanId == userId && s.StartDelivery.Month == date.Month)
                .ProjectTo<DeliverymanScheduleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return schedule;
        }

    }
}