using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace LoggingManagerAdapters.Strategies
{
    public class LoginUserStrategy : IProcedureStrategy
    {

        private OracleCommand command;

        public LoginUserStrategy(OracleCommand command)
        {
            this.command = command;
        }

        public OracleProcedureResponse<TOutput>? executeProcedure<TOutput>()
        {

            command.ExecuteNonQuery();

            int errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string errorMessage = command.Parameters["o_error_message"].Value.ToString()!;

            User user = new User();

            if (errorCode == 0)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    user.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    user.Username = reader.GetString(reader.GetOrdinal("Username"));
                    user.UserType.UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));
                    user.UserType.Type = reader.GetString(reader.GetOrdinal("UserType"));
                }

                return new OracleProcedureResponse<User>(errorCode, errorMessage, user) as OracleProcedureResponse<TOutput>;
            }
            else
            {
                return new OracleProcedureResponse<User>(errorCode, errorMessage) as OracleProcedureResponse<TOutput>;
            }
        }

        public void setParameters<TInput>(TInput inputs)
        {
            Credential? credential = inputs as Credential;

            // Input parameter
            command.Parameters.Add("p_username", OracleDbType.NVarchar2).Value = credential.Username;
            command.Parameters.Add("p_password", OracleDbType.NVarchar2).Value = credential.Password;

            // Output parameters
            command.Parameters.Add("o_response_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            command.Parameters.Add("o_error_code", OracleDbType.Int32).Direction = ParameterDirection.Output;
            command.Parameters.Add("o_error_message", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
        }
    }
}
