using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Enums;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAdapters.Repositories
{
    public class SystemRepository : ISystemRepository
    {
        IOracleDbContext context;

        public SystemRepository(IOracleDbContext context)
        {
            this.context = context;
        }

        public GenericResponse<string>? Create(CreateSystemDto createSystemDto)
        {
            return context.ExecuteStoreProcedure<CreateSystemDto, GenericResponse<string>>(StoreProcedure.EnrollNewSystem,createSystemDto);
        }

        public GenericResponse<List<EnrollSystem>>? getAll()
        {
            return context.ExecuteStoreProcedure<GenericResponse<List<EnrollSystem>>>(StoreProcedure.GetEnrolledSystems);
        }

        public GenericResponse<List<ErrorLog>>? GetErrorLogs(int systemId)
        {
            return context.ExecuteStoreProcedure<int, GenericResponse<List<ErrorLog>>>(StoreProcedure.GetErrorLogs,systemId);
        }

        public GenericPaginatedResponse<List<ErrorLog>>? GetErrorLogs(GetLogDto getErrorLogDto)
        {
            return context.ExecuteStoreProcedure<GetLogDto, GenericPaginatedResponse<List<ErrorLog>>>(StoreProcedure.GetPaginatedErrorLogs,getErrorLogDto);
        }

        public GenericPaginatedResponse<List<TrackingLog>>? GetTrackingLogs(GetLogDto getTrackingLogDto)
        {
            return context.ExecuteStoreProcedure<GetLogDto, GenericPaginatedResponse<List<TrackingLog>>>(StoreProcedure.GetPaginatedTrackingLogs, getTrackingLogDto);
        }

        public GenericResponse<string>? Update(UpdateSystemDto updateSystemDto)
        {
            return context.ExecuteStoreProcedure<UpdateSystemDto, GenericResponse<string>>(StoreProcedure.UpdateSystem ,updateSystemDto);
        }
    }
}
