namespace LoggingManagerCore.Dtos
{
    public class GenericResponse<T>
    {
        public T? Data { get; set; }
        public int? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; } = string.Empty;


        public GenericResponse(int? errorCode, string? errorMessage, T? responseData)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Data = responseData;
        }

        public GenericResponse(int? errorCode, string? errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public GenericResponse()
        {

        }
    }
}
