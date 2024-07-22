using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface ISystemService
    {
        GenericResponse<List<EnrollSystem>>? getAll();

        GenericResponse<string>? Create(CreateSystemDto createSystemDto);

        GenericResponse<string>? Update(UpdateSystemDto updateSystemDto);

        GenericResponse<List<ErrorLog>>? GetErrorLogs(int systemId);

        GenericPaginatedResponse<List<ErrorLog>>? GetErrorLogs(GetLogDto errorLogDto);

        GenericPaginatedResponse<List<TrackingLog>>? GetTrackingLogs(GetLogDto trackingLogDto);

    }
}
