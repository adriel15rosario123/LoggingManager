using LoggingManagerCore.Entities;
using LoggingManagerCore.Enums;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace LoggingManagerAdapters.Helpers
{
    public static class OracleProcedureExecutor
    {
        public static OracleProcedureResponse<T>? TriggerProcedure<T>(StoreProcedure procedure, OracleCommand cmd)
        {
            cmd.ExecuteNonQuery();

            int errorCode = ((OracleDecimal)cmd.Parameters["o_error_code"].Value).ToInt32();
            string errorMessage = cmd.Parameters["o_error_message"].Value.ToString()!;

            switch (procedure)
            {
                case StoreProcedure.GetUserByUsername:

                    return ExecuteGetUserByUsernameProc(cmd, errorCode, errorMessage) as OracleProcedureResponse<T>;
                case StoreProcedure.LoginUser:
                    return ExecuteLoginUserProc(cmd, errorCode, errorMessage) as OracleProcedureResponse<T>;
            }

            return new OracleProcedureResponse<T>();
        }

        private static OracleProcedureResponse<User> ExecuteGetUserByUsernameProc(OracleCommand cmd, int errorCode, string errorMessage)
        {

            User user = new User();

            //retrive the output parameters
            if (errorCode == 0)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)cmd.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    user.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    user.Username = reader.GetString(reader.GetOrdinal("Username"));
                    user.UserType.UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));
                    user.UserType.Type = reader.GetString(reader.GetOrdinal("UserType"));
                }

                return new OracleProcedureResponse<User>(errorCode, errorMessage, user);
            }
            else
            {
                return new OracleProcedureResponse<User>(errorCode, errorMessage);
            }
        }

        private static OracleProcedureResponse<User> ExecuteLoginUserProc(OracleCommand cmd, int errorCode, string errorMessage)
        {
            User user = new User();

            if (errorCode == 0)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)cmd.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    user.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    user.Username = reader.GetString(reader.GetOrdinal("Username"));
                    user.UserType.UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));
                    user.UserType.Type = reader.GetString(reader.GetOrdinal("UserType"));
                }

                return new OracleProcedureResponse<User>(errorCode, errorMessage, user);
            }
            else
            {
                return new OracleProcedureResponse<User>(errorCode, errorMessage);
            }

        }
    }

}
