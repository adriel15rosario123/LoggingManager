using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Enums;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAdapters.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        IOracleDbContext context;

        public SystemRepository(IOracleDbContext context)
        {
            this.context = context;
        }

        public GenericResponse<string>? Create(CreateSystemDto createSystemDto)
        {
            return context.ExecuteStoreProcedure<CreateSystemDto,string>(StoreProcedure.EnrollNewSystem,createSystemDto);
        }

        public GenericResponse<List<EnrollSystem>>? getAll()
        {
            return context.ExecuteStoreProcedure<List<EnrollSystem>>(StoreProcedure.GetEnrolledSystems);
        }
    }
}
