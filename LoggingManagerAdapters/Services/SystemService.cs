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
        public OracleProcedureResponse<List<EnrollSystem>>? getAll()
        {
            return systemRepository.getAll();
        }
    }
}
