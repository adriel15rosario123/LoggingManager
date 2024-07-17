using LoggingManagerCore.Dtos;
using LoggingManagerCore.Enums;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IOracleDbContext
    {
        GenericResponse<TOutput>? ExecuteStoreProcedure<TInput, TOutput>(StoreProcedure procedure, TInput inputData);

        GenericResponse<TOutput>? ExecuteStoreProcedure<TOutput>(StoreProcedure procedure);
    }
}
