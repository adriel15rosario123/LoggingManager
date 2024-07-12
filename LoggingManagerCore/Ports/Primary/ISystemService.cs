using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface ISystemService
    {
        OracleProcedureResponse<List<EnrollSystem>>? getAll();
    }
}
