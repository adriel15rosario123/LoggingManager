using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Primary;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAdapters.Services
{
    public class SystemService : ISystemService
    {
        private ISystemRepository systemRepository;

        public SystemService(ISystemRepository systemRepository)
        {
            this.systemRepository = systemRepository;
        }

        public GenericResponse<string>? Create(CreateSystemDto createSystemDto)
        {
            return systemRepository.Create(createSystemDto);
        }

        public GenericResponse<List<EnrollSystem>>? getAll()
        {
            return systemRepository.getAll();
        }

        public GenericResponse<string>? Update(UpdateSystemDto updateSystemDto)
        {
            return systemRepository.Update(updateSystemDto);
        }
    }
}
