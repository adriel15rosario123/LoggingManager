using LoggingManagerAdapters.Strategies;
using LoggingManagerCore.Enums;
using LoggingManagerCore.Ports.Secundary;
using LoggingManagerCore.Utilities;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LoggingManagerAdapters.Helpers
{
    public class OracleProcedureHandler
    {

        private OracleCommand command;
        public IProcedureStrategy ProcedureStrategy { get; }

        public OracleProcedureHandler(StoreProcedure procedure, OracleConnection connection, string schema)
        {
            string storeProcedure = $"{schema}.{EnumHandler.GetValue(procedure)}";
            command = new OracleCommand(storeProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            ProcedureStrategy = GetStrategy(procedure);
        }

        private IProcedureStrategy GetStrategy(StoreProcedure procedure)
        {
            return procedure switch
            {
                StoreProcedure.LoginUser => new LoginUserStrategy(command),
                StoreProcedure.GetUserByUsername => new GetUserByUsernameStrategy(command),
                _ => throw new ArgumentException($"Invalid procedure: {EnumHandler.GetValue(procedure)}")
            } ;
        }
    }
}
