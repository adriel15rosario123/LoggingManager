using LoggingManagerAdapters.Helpers;
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

        public TOutput? ExecuteStoreProcedure<TInput, TOutput>(StoreProcedure procedure, TInput inputData) where TOutput : class
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleProcedureHandler procedureHandler = new OracleProcedureHandler(procedure, connection, schema);

                procedureHandler.ProcedureStrategy.setParameters(inputData);

                TOutput? response = procedureHandler.ProcedureStrategy.executeProcedure<TOutput>();

                connection.Close();

                return response;
            }
        }

        public TOutput? ExecuteStoreProcedure<TOutput>(StoreProcedure procedure) where TOutput : class
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                OracleProcedureHandler procedureHandler = new OracleProcedureHandler(procedure, connection, schema);

                procedureHandler.ProcedureStrategy.setParameters<object>();

                TOutput? response = procedureHandler.ProcedureStrategy.executeProcedure<TOutput>();

                connection.Close();

                return response;
            }
        }


    }


}
