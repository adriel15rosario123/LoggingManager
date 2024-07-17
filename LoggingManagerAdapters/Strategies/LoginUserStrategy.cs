using LoggingManagerCore.Dtos;
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

        public GenericResponse<TOutput>? executeProcedure<TOutput>()
        {

            command.ExecuteNonQuery();

            int? errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string? errorMessage = ((OracleString)command.Parameters["o_error_message"].Value).IsNull ? null : command.Parameters["o_error_message"].Value.ToString();

            User user = new User();

            if (errorCode is null)
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

                return new GenericResponse<User>(errorCode, errorMessage, user) as GenericResponse<TOutput>;
            }
            else
            {
                return new GenericResponse<User>(errorCode, errorMessage) as GenericResponse<TOutput>;
            }
        }

        public void setParameters<TInput>(TInput? inputs = default)
        {
          
            OracleParameter[] parameters =
            [

                //Input params
                new OracleParameter()
                {
                    ParameterName = "p_username",
                    OracleDbType = OracleDbType.NVarchar2,
                    Value = (inputs as Credential)!.Username,
                    IsNullable = true
                },
                new OracleParameter()
                {
                    ParameterName = "p_password",
                    OracleDbType = OracleDbType.NVarchar2,
                    Value = (inputs as Credential)!.Password,
                    IsNullable = true
                },

                //Output params
                new OracleParameter()
                {
                    ParameterName = "o_response_data",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output,
                    IsNullable = true
                },
                new OracleParameter()
                {
                    ParameterName = "o_error_code",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Output,
                    IsNullable = true
                },
                new OracleParameter(){
                    ParameterName = "o_error_message",
                    OracleDbType = OracleDbType.Varchar2,
                    Direction = ParameterDirection.Output,
                    Size = 200,
                    IsNullable = true
                },
            ];

            command.Parameters.AddRange( parameters );
        }
    }
}
