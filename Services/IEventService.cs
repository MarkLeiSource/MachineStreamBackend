using MachineStreamBackend.Dtos;

namespace MachineStreamBackend.Services
{
    public interface IEventService
    {
        List<EventDto> GetEvents(DateTime from, DateTime to, string? machineId, int? pageIndex, int? pageSize);
        void SaveEvent(EventDto eventDto);
    }
}
