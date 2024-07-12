using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface ISystemRepository
    {
        OracleProcedureResponse<List<EnrollSystem>>? getAll();

    }
}
