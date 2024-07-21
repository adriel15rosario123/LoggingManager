using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Secundary
{
    public interface ISystemRepository
    {
        GenericResponse<List<EnrollSystem>>? getAll();

        GenericResponse<string>? Create(CreateSystemDto createSystemDto);

        GenericResponse<string>? Update(UpdateSystemDto updateSystemDto);

        GenericResponse<List<ErrorLog>>? GetErrorLogs(int systemId);

        GenericPaginatedResponse<List<ErrorLog>>? GetErrorLogs(GetErrorLogDto getErrorLogDto);
    }
}
