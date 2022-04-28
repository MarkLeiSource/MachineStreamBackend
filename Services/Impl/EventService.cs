using MachineStreamBackend.Daos.Repo;
using MachineStreamBackend.Dtos;
using MachineStreamBackend.Entities;
using MachineStreamBackend.Utils;

namespace MachineStreamBackend.Services.Impl
{
    public class EventService : IEventService
    {
        private IEventReposity EventReposity { get; set; }
        private AutoMapperUtil _mapperUtil;
        public EventService(IEventReposity eventReposity, AutoMapperUtil autoMapperUtil)
        {
            EventReposity = eventReposity;
            _mapperUtil = autoMapperUtil;
        }
        public List<EventDto> GetEvents(DateTime from, DateTime to, string machineId, int? pageIndex, int? pageSize)
        {
            var entities = EventReposity.GetEvents(from, to, machineId, pageIndex, pageSize);
            var list = entities.Select(e => _mapperUtil.Mapper.Map<EventDto>(e)).ToList();
            return list;
        }

        public async void SaveEvent(EventDto eventDto)
        {
            var entity = _mapperUtil.Mapper.Map<EventEntity>(eventDto);
            await EventReposity.AddAsync(entity);
        }
    }
}
