using LoggingManagerCore.Entities;
using LoggingManagerCore.Enums;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IOracleDbContext
    {
        OracleProcedureResponse<TOutput>? ExecuteStoreProcedure<TInput, TOutput>(StoreProcedure procedure, TInput inputData);
    }
}
