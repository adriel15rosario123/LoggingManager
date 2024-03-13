namespace LoggingManagerCore.Entities
{
    public class OracleProcedureResponse<T>
    {
        public T? ResponseData { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        

        public OracleProcedureResponse(int errorCode, string errorMessage, T responseData)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ResponseData = responseData;
        }

        public OracleProcedureResponse(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public OracleProcedureResponse()
        {
   
        }
    }
}
