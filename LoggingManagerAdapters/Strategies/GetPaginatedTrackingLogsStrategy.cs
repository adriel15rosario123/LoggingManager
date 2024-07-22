using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingManagerAdapters.Strategies
{
    public class GetPaginatedTrackingLogsStrategy : IProcedureStrategy
    {
        private OracleCommand command;

        public GetPaginatedTrackingLogsStrategy(OracleCommand command)
        {
            this.command = command;
        }

        public TOutput? executeProcedure<TOutput>() where TOutput : class
        {
            command.ExecuteNonQuery();

            int? pageNumber = ((OracleDecimal)command.Parameters["o_page_number"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_page_number"].Value).ToInt32();
            int? pageSize = ((OracleDecimal)command.Parameters["o_page_size"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_page_size"].Value).ToInt32();
            int? totalPages = ((OracleDecimal)command.Parameters["o_total_pages"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_total_pages"].Value).ToInt32();
            int? totalRecords = ((OracleDecimal)command.Parameters["o_total_records"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_total_records"].Value).ToInt32();
            int? errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string? errorMessage = ((OracleString)command.Parameters["o_error_message"].Value).IsNull ? null : command.Parameters["o_error_message"].Value.ToString();

            List<TrackingLog> trackingLogs = new List<TrackingLog>();

            //retrive the output parameters
            if (errorCode is null)
            {
                // Successful execution, retrieve data from the cursor
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["o_response_data"].Value).GetDataReader();

                while (reader.Read())
                {
                    TrackingLog trackinLog = new TrackingLog
                    {
                        LogId = reader.GetInt32(reader.GetOrdinal("LogId")),
                        LoggingDate = reader.GetDateTime(reader.GetOrdinal("LoggingDate")),
                        MethodName = reader.IsDBNull(reader.GetOrdinal("MethodName")) ? null : reader.GetString(reader.GetOrdinal("MethodName")),
                        MethodInput = reader.IsDBNull(reader.GetOrdinal("MethodInput")) ? null : reader.GetString(reader.GetOrdinal("MethodInput")),
                        MethodOutput = reader.IsDBNull(reader.GetOrdinal("MethodOutput")) ? null : reader.GetString(reader.GetOrdinal("MethodOutput")),
                        LogType = reader.GetString(reader.GetOrdinal("LogType")),
                        Message = reader.IsDBNull(reader.GetOrdinal("Message")) ? null : reader.GetString(reader.GetOrdinal("Message"))
                    };

                    trackingLogs.Add(trackinLog);
                }

                return new GenericPaginatedResponse<List<TrackingLog>>(errorCode, errorMessage, trackingLogs, pageNumber, pageSize, totalPages, totalRecords) as TOutput;
            }
            else
            {
                return new GenericPaginatedResponse<List<TrackingLog>>(errorCode, errorMessage, pageNumber, pageSize, totalPages, totalRecords) as TOutput;
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
                    Value = (inputs as GetLogDto)!.SystemId,
                    IsNullable = false,
                },
                new OracleParameter()
                {
                    ParameterName = "p_page_number",
                    OracleDbType = OracleDbType.Int64,
                    Value = (inputs as GetLogDto)!.PageNumber,
                    IsNullable = false,
                },
                new OracleParameter()
                {
                    ParameterName = "p_page_size",
                    OracleDbType = OracleDbType.Int64,
                    Value = (inputs as GetLogDto)!.PageSize,
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
                    ParameterName = "o_total_records",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Output,
                    IsNullable = true
                },
                new OracleParameter()
                {
                    ParameterName = "o_total_pages",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Output,
                    IsNullable = true
                },
                new OracleParameter()
                {
                    ParameterName = "o_page_number",
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Output,
                    IsNullable = true
                },
                new OracleParameter()
                {
                    ParameterName = "o_page_size",
                    OracleDbType = OracleDbType.Int32,
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
