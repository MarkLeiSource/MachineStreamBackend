using MachineStreamBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineStreamBackend.Daos
{
    public class EventStreamDbContext: DbContext
    {
        public DbSet<EventEntity> Events { get; set; }


        public EventStreamDbContext(DbContextOptions<EventStreamDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EventEntity>().ToTable("Event").HasKey(e=>new { e.Machine_id, e.Id });
        }
    }
}
