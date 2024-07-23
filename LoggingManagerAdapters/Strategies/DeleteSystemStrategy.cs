using LoggingManagerCore.Dtos;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace LoggingManagerAdapters.Strategies
{
    public class DeleteSystemStrategy : IProcedureStrategy
    {
        private OracleCommand command;

        public DeleteSystemStrategy(OracleCommand command)
        {
            this.command = command;
        }

        public TOutput? executeProcedure<TOutput>() where TOutput : class
        {
            command.ExecuteNonQuery();

            int? errorCode = ((OracleDecimal)command.Parameters["o_error_code"].Value).IsNull ? null : ((OracleDecimal)command.Parameters["o_error_code"].Value).ToInt32();
            string? errorMessage = ((OracleString)command.Parameters["o_error_message"].Value).IsNull ? null : command.Parameters["o_error_message"].Value.ToString();
            string? responseData = ((OracleString)command.Parameters["o_response_data"].Value).IsNull ? null : command.Parameters["o_response_data"].Value.ToString();

            return new GenericResponse<string>(errorCode, errorMessage, responseData) as TOutput;
        }

        public void setParameters<TInput>(TInput? inputs = default)
        {
            OracleParameter[] parameters =
           [

                //Input params
                 new OracleParameter()
                {
                    ParameterName = "p_enrolled_system_id",
                    OracleDbType = OracleDbType.Int64,
                    Value = inputs is int ? (int)(object)inputs : 0,
                    IsNullable = false,
                },

                //Output params
                new OracleParameter()
                {
                    ParameterName = "o_response_data",
                    OracleDbType = OracleDbType.NVarchar2,
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
