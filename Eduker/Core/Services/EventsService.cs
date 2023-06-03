using Contract.ClientDto;
using Domain.Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Contract.AdminDto.EventsInfoDto;

namespace Services
{
    public class EventsService : IEventsService
    {
        private readonly IRepositoryManager _repositoryManager;

        public EventsService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<EventsDto>> GetAllAsync()
        {
            var entities = await _repositoryManager.EventsRepository.GetAllAsync();

            return entities.Select(e => new EventsDto
            {
                Id = e.Id,
                Name = e.Name,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                EducatorName = e.EducatorName,
                Address = e.Address,
                ImgPath=e.ImgPath
            });
        }

        public async Task<EventDetailsDto> GetInfoAsync(int id)
        {
            var entity = await _repositoryManager.EventsRepository.GetAsync(id);

            return new EventDetailsDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                TimeStart = entity.TimeStart,
                TimeEnd = entity.TimeEnd,
                EducatorName = entity.EducatorName,
                Address = entity.Address,
                Price = entity.Price,
                ImgPath = entity.ImgPath
            };
        }
        public async Task DeleteEventsInfo(int id)
        {
            await _repositoryManager.EventsRepository.DeleteEventsInfo(id);
        }

        public async Task AddEventsInfo(AddEventsInfoDto eventsInfoDto)
        {
            var newEvents = new Events
            {
                Name = eventsInfoDto.Name,
                Address = eventsInfoDto.Address,
                Description = eventsInfoDto.Description,
                TimeStart= eventsInfoDto.TimeStart,
                TimeEnd= eventsInfoDto.TimeEnd,
                EducatorName= eventsInfoDto.EducatorName,
                Language=eventsInfoDto.Language,
                Price=eventsInfoDto.Price,
                ImgPath= eventsInfoDto.ImgPath

            };

            await _repositoryManager.EventsRepository.AddEventsInfo(newEvents);
        }

        public async Task<bool> EditEventsInfo(EditEventsInfoDto eventsInfodto)
        {
            var newEventsInfo = new Events
            {
                Id = eventsInfodto.Id,
                Name = eventsInfodto.Name,
                Address = eventsInfodto.Address,
                Description = eventsInfodto.Description,
                TimeStart = eventsInfodto.TimeStart,
                TimeEnd = eventsInfodto.TimeEnd,
                EducatorName = eventsInfodto.EducatorName,
                Language = eventsInfodto.Language,
                Price = eventsInfodto.Price,
                ImgPath = eventsInfodto.ImgPath

            };
            var result = await _repositoryManager.EventsRepository.EditEventsInfo(newEventsInfo);

            if (result == true)
            {
                return true;
            }

            return false;
        }
    }
}