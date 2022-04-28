using MachineStreamBackend.Entities;

namespace MachineStreamBackend.Daos.Repo
{
    public interface IEventReposity
    {
        Task<int> AddAsync(EventEntity entity);

        List<EventEntity> GetEvents(DateTime from, DateTime to, string machineId, int? pageIndex, int? pageSize);
    }
}
