using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.ClientDto;
using Contract.AdminDto.EventsInfoDto;

namespace Services.Abstraction
{
    public interface IEventsService
    {
        public Task<IEnumerable<EventsDto>> GetAllAsync();
        public Task<EventDetailsDto> GetInfoAsync(int id);
        public Task<bool> EditEventsInfo(EditEventsInfoDto eventsInfoDto);
        public Task AddEventsInfo(AddEventsInfoDto eventsInfoDto);
        public Task DeleteEventsInfo(int id);
    }
}