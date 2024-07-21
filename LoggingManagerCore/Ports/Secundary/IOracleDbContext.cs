using LoggingManagerCore.Dtos;
using LoggingManagerCore.Enums;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IOracleDbContext
    {
        TOutput? ExecuteStoreProcedure<TInput, TOutput>(StoreProcedure procedure, TInput inputData)  where TOutput : class;

        TOutput? ExecuteStoreProcedure<TOutput>(StoreProcedure procedure) where TOutput:class;

        //TOutput ExecuteStoreProcedure<TInput, TOutput>(StoreProcedure procedure, TInput inputData, string test);
    }
}
