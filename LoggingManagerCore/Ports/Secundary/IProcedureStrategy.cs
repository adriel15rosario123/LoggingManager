using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IProcedureStrategy
    {
        void setParameters<TInput>(TInput inputs);

        OracleProcedureResponse<TOutput>? executeProcedure<TOutput>();
    }
}
