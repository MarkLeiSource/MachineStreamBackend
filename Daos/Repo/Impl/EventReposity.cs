using MachineStreamBackend.Entities;
using System.Linq.Expressions;

namespace MachineStreamBackend.Daos.Repo.Impl
{
    public class EventReposity : IEventReposity
    {
        private IServiceProvider _serviceProvider;
        public EventReposity(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<int> AddAsync(EventEntity entity)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<EventStreamDbContext>();
                await dbContext.AddAsync(entity);
                return await dbContext.SaveChangesAsync();
            }
        }

        public List<EventEntity> GetEvents(DateTime from, DateTime to, string machineId, int? pageIndex, int? pageSize)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<EventStreamDbContext>();
                Func<EventEntity, bool> predicate;
                if (!string.IsNullOrEmpty(machineId))
                {
                    predicate = (e => e.Machine_id.Equals(machineId) && e.Timestamp >= from && e.Timestamp < to);
                }
                else
                {
                    predicate =  (e => e.Timestamp >= from && e.Timestamp < to);
                }
                var query = dbContext.Events.Where(predicate);
                if (pageSize == null)
                {
                    pageSize = 100;
                }
                if (pageIndex != null && pageIndex > 0)
                {
                    query = query.Skip(pageIndex.Value * pageSize.Value).Take(pageSize.Value);
                }
                return query.ToList();
            }
        }
    }
}
