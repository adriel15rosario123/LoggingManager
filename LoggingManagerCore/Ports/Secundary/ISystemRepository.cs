using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface ISystemRepository
    {
        GenericResponse<List<EnrollSystem>>? getAll();

        GenericResponse<string>? Create(CreateSystemDto createSystemDto);
    }
}
