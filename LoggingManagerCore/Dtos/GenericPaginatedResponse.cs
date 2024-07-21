namespace LoggingManagerCore.Dtos
{
    public class GenericPaginatedResponse<T> : GenericResponse<T>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalRecords { get; set; }

        public GenericPaginatedResponse(int? errorCode, string? errorMessage, int? pageNumber, int? pageSize, int? totalPages, int? totalRecords) : base(errorCode, errorMessage)
        {

            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }

        public GenericPaginatedResponse(int? errorCode, string? errorMessage, T? responseData, int? pageNumber, int? pageSize, int? totalPages, int? totalRecords) : base(errorCode, errorMessage, responseData)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}
