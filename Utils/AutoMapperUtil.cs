using AutoMapper;
using MachineStreamBackend.Dtos;
using MachineStreamBackend.Entities;

namespace MachineStreamBackend.Utils
{
    public class AutoMapperUtil
    {
        public IMapper Mapper { get; private set; }
        public AutoMapperUtil()
        {
            var configuration = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<EventEntity, EventDto>();
                cfg.CreateMap<EventDto, EventEntity>();
             });
            configuration.AssertConfigurationIsValid();
            Mapper = configuration.CreateMapper();
        }
    }
}
