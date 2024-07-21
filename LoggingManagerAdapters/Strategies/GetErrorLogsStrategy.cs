using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace LoggingManagerAdapters.Strategies
{
    public class GetErrorLogsStrategy : IProcedureStrategy
    {
        private OracleCommand command;

        public GetErrorLogsStrategy(OracleCommand command)
        {
            this.command = command;
        }

        public TOutput? executeProcedure<TOutput>() where TOutput : class
        {
            command.ExecuteNonQuery();

            int? errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string? errorMessage = ((OracleString)command.Parameters["o_error_message"].Value).IsNull ? null : command.Parameters["o_error_message"].Value.ToString();

            List<ErrorLog> errorLogs = new List<ErrorLog>();

            //retrive the output parameters
            if (errorCode is null)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    ErrorLog errorLog = new ErrorLog
                    {
                        LogId = reader.GetInt32(reader.GetOrdinal("LogId")),
                        LoggingDate = reader.GetDateTime(reader.GetOrdinal("LoggingDate")),
                        MethodName = reader.IsDBNull(reader.GetOrdinal("MethodName")) ? null: reader.GetString(reader.GetOrdinal("MethodName")),
                        MethodInput = reader.IsDBNull(reader.GetOrdinal("MethodInput")) ? null : reader.GetString(reader.GetOrdinal("MethodInput")),
                        MethodOutput = reader.IsDBNull(reader.GetOrdinal("MethodOutput"))? null : reader.GetString(reader.GetOrdinal("MethodOutput")),
                        LogType = reader.GetString(reader.GetOrdinal("LogType")),
                        Message = reader.IsDBNull(reader.GetOrdinal("Message")) ? null : reader.GetString(reader.GetOrdinal("Message"))
                    };

                    errorLogs.Add(errorLog);
                }

                return new GenericResponse<List<ErrorLog>>(errorCode, errorMessage, errorLogs) as TOutput;
            }
            else
            {
                return new GenericResponse<List<ErrorLog>>(errorCode, errorMessage) as TOutput;
            }
        }

        public void setParameters<TInput>(TInput? inputs = default)
        {
            OracleParameter[] parameters =
            [

               //Input params
               new OracleParameter()
                {
                    ParameterName = "p_system_id",
                    OracleDbType = OracleDbType.Int64,
                    Value = inputs is int ? (int)(object)inputs : 0,
                    IsNullable = false,
                },

                //Output params
                new OracleParameter()
                {
                    ParameterName = "o_response_data",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output,
                    Size = 200,
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
                    OracleDbType = OracleDbType.NVarchar2,
                    Direction = ParameterDirection.Output,
                    Size = 200,
                    IsNullable = true
                },
            ];

            command.Parameters.AddRange(parameters);
        }
    }
}
