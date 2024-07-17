using LoggingManagerCore.Dtos;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface IProcedureStrategy
    {
        void setParameters<TInput>(TInput? inputs = default);

        GenericResponse<TOutput>? executeProcedure<TOutput>();
    }
}
