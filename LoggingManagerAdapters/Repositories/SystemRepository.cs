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

        public OracleProcedureResponse<List<EnrollSystem>>? getAll()
        {
            return context.ExecuteStoreProcedure<List<EnrollSystem>>(StoreProcedure.GetEnrolledSystems);
        }
    }
}
