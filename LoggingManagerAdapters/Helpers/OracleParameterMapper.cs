using LoggingManagerCore.Entities;
using LoggingManagerCore.Enums;
using LoggingManagerCore.Utilities;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LoggingManagerAdapters.Helpers
{
    public static class OracleParameterMapper
    {
        public static void SetOracleParameters<T>(StoreProcedure procedure, OracleCommand cmd, T inputs)
        {
            switch (procedure)
            {
                case StoreProcedure.GetUserByUsername:

                    if (typeof(T) == typeof(string))
                    {
                        GetUserByUsernameParams(cmd, inputs as string);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid input type for the procedure: {EnumHandler.GetValue(procedure)}");
                    }
                    break;
                case StoreProcedure.LoginUser:
                    if (typeof(T) == typeof(Credential))
                    {
                        LoginUserParams(cmd, inputs as Credential);
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid input type for the procedure: {EnumHandler.GetValue(procedure)}");
                    }
                    break;
            }
        }

        private static void GetUserByUsernameParams(OracleCommand cmd, string username)
        {
            // Input parameter
            cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

            // Output parameters
            cmd.Parameters.Add("o_response_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("o_error_code", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("o_error_message", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
        }

        private static void LoginUserParams(OracleCommand cmd, Credential credential)
        {
            // Input parameter
            cmd.Parameters.Add("p_username", OracleDbType.NVarchar2).Value = credential.Username;
            cmd.Parameters.Add("p_password", OracleDbType.NVarchar2).Value = credential.Password;

            // Output parameters
            cmd.Parameters.Add("o_response_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("o_error_code", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("o_error_message", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
        }
    }
}
