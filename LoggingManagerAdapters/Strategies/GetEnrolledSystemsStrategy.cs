using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace LoggingManagerAdapters.Strategies
{
    public class GetEnrolledSystemsStrategy : IProcedureStrategy
    {
        private OracleCommand command;

        public GetEnrolledSystemsStrategy(OracleCommand command)
        {
            this.command = command;
        }
        public OracleProcedureResponse<TOutput>? executeProcedure<TOutput>()
        {
            command.ExecuteNonQuery();

            int errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string errorMessage = command.Parameters["o_error_message"].Value.ToString()!;

            List<EnrollSystem> enrolledSystems = new List<EnrollSystem>();

            //retrive the output parameters
            if (errorCode == 0)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    EnrollSystem enrollSystem = new EnrollSystem
                    {
                        EnrolledSystemId = reader.GetInt32(reader.GetOrdinal("EnrolledSystemId")),
                        SystemName = reader.GetString(reader.GetOrdinal("SystemName")),
                        EnrolledDate = reader.GetDateTime(reader.GetOrdinal("EnrolledDate")),
                        LastUpdatedDate = reader.IsDBNull(reader.GetOrdinal("LastUpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LastUpdatedDate")),
                        ErrorLogs = reader.GetInt32(reader.GetOrdinal("ErrorLogs")),
                        TrackingLogs = reader.GetInt32(reader.GetOrdinal("TrackingLogs"))
                    };

                    enrolledSystems.Add(enrollSystem);
                }

                return new OracleProcedureResponse<List<EnrollSystem>>(errorCode, errorMessage, enrolledSystems) as OracleProcedureResponse<TOutput>;
            }
            else
            {
                return new OracleProcedureResponse<User>(errorCode, errorMessage) as OracleProcedureResponse<TOutput>;
            }
        }

        public void setParameters<TInput>(TInput inputs = default)
        {
            // Output parameters
            command.Parameters.Add("o_response_data", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            command.Parameters.Add("o_error_code", OracleDbType.Int32).Direction = ParameterDirection.Output;
            command.Parameters.Add("o_error_message", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
        }
    }
}
