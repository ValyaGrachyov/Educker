using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EduckerDbContext _dbContext;

        public EventsRepository(EduckerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            return await _dbContext.Events.ToListAsync();
        }

        public async Task<Events> GetAsync(int id)
        {
            return await _dbContext.Events.FindAsync(id);
        }
        public async Task DeleteEventsInfo(int id)
        {
            var events = await GetAsync(id);
            _dbContext.Events.Remove(events);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddEventsInfo(Events events)
        {
            await _dbContext.Events.AddAsync(events);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EditEventsInfo(Events events)
        {
            Events newEventsInfo = await _dbContext.Events.FindAsync(events.Id);
            newEventsInfo = Edit(newEventsInfo, events);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private Events Edit(Events oldEventsInfo, Events newEventsInfo)
        {
            oldEventsInfo.Name = newEventsInfo.Name;
            oldEventsInfo.Address = newEventsInfo.Address;
            oldEventsInfo.Description = newEventsInfo.Description;
            oldEventsInfo.TimeStart = newEventsInfo.TimeStart;
            oldEventsInfo.TimeEnd = newEventsInfo.TimeEnd;
            oldEventsInfo.EducatorName = newEventsInfo.EducatorName;
            oldEventsInfo.Language = newEventsInfo.Language;
            oldEventsInfo.Price = newEventsInfo.Price;
            return oldEventsInfo;
        }
    }
}