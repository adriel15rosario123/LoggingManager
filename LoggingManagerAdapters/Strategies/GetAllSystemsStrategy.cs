using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace LoggingManagerAdapters.Strategies
{
    public class GetAllSystemsStrategy : IProcedureStrategy
    {
        private OracleCommand command;

        public GetAllSystemsStrategy(OracleCommand command)
        {
            this.command = command;
        }
        public GenericResponse<TOutput>? executeProcedure<TOutput>()
        {
            command.ExecuteNonQuery();

            int? errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string? errorMessage = ((OracleString)command.Parameters["o_error_message"].Value).IsNull ? null: command.Parameters["o_error_message"].Value.ToString();

            List<EnrollSystem> enrolledSystems = new List<EnrollSystem>();

            //retrive the output parameters
            if (errorCode is null)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    EnrollSystem enrollSystem = new EnrollSystem
                    {
                        SystemId = reader.GetInt32(reader.GetOrdinal("EnrolledSystemId")),
                        SystemUsername = reader.GetString(reader.GetOrdinal("SystemUsername")),
                        SystemPassword = reader.GetString(reader.GetOrdinal("SystemPassword")),
                        SystemName = reader.GetString(reader.GetOrdinal("SystemName")),
                        EnrolledDate = reader.GetDateTime(reader.GetOrdinal("EnrolledDate")),
                        LastUpdatedDate = reader.IsDBNull(reader.GetOrdinal("LastUpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LastUpdatedDate")),
                        ErrorLogs = reader.GetInt32(reader.GetOrdinal("ErrorLogs")),
                        TrackingLogs = reader.GetInt32(reader.GetOrdinal("TrackingLogs"))
                    };

                    enrolledSystems.Add(enrollSystem);
                }

                return new GenericResponse<List<EnrollSystem>>(errorCode, errorMessage, enrolledSystems) as GenericResponse<TOutput>;
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


            command.Parameters.AddRange(parameters);

        }
    }
}
