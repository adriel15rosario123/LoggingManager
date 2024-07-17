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
        EnrollNewSystem
    }
}
