using LoggingManagerCore.Utilities;

namespace LoggingManagerCore.Enums
{
 
    public enum StoreProcedure
    {
        [EnumStringValue("GET_USER_BY_USERNAME")]
        GetUserByUsername,

        [EnumStringValue("LOGIN_USER")]
        LoginUser,

        [EnumStringValue("GET_ENROLLED_SYSTEMS")]
        GetEnrolledSystems,

        [EnumStringValue("ENROLL_NEW_SYSTEM")]
        EnrollNewSystem,

        [EnumStringValue("UPDATE_SYSTEM")]
        UpdateSystem,

        [EnumStringValue("GET_ERROR_LOGS")]
        GetErrorLogs,

        [EnumStringValue("GET_PAGINATED_ERROR_LOGS")]
        GetPaginatedErrorLogs,

        [EnumStringValue("GET_PAGINATED_TRACKING_LOGS")]
        GetPaginatedTrackingLogs,

        [EnumStringValue("DELETE_ENROLLED_SYSTEM")]
        DeleteSystem
    }
}
