using LoggingManagerAdapters.Helpers;
using LoggingManagerCore.Dtos;
using LoggingManagerCore.Enums;
using LoggingManagerCore.Ports.Secundary;
using Oracle.ManagedDataAccess.Client;

namespace LoggingManagerAdapters.Repositories
{
    public class OracleDbContext : IOracleDbContext
    {
        private string connectionString;
        private string schema;

        public OracleDbContext(string connectionString, string schema)
        {
            this.connectionString = connectionString;
            this.schema = schema;
        }

        public GenericResponse<TOutput>? ExecuteStoreProcedure<TInput, TOutput>(StoreProcedure procedure, TInput inputData)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleProcedureHandler procedureHandler = new OracleProcedureHandler(procedure, connection, schema);

                procedureHandler.ProcedureStrategy.setParameters(inputData);

                GenericResponse<TOutput>? response = procedureHandler.ProcedureStrategy.executeProcedure<TOutput>();

                connection.Close();

                return response;
            }
        }

        public GenericResponse<TOutput>? ExecuteStoreProcedure<TOutput>(StoreProcedure procedure)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleProcedureHandler procedureHandler = new OracleProcedureHandler(procedure, connection, schema);

                procedureHandler.ProcedureStrategy.setParameters<object>();

                GenericResponse<TOutput>? response = procedureHandler.ProcedureStrategy.executeProcedure<TOutput>();

                connection.Close();

                return response;
            }
        }
    }


}
