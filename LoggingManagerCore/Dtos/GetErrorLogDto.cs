using System.Text.Json.Serialization;

namespace LoggingManagerCore.Dtos
{
    public class GetErrorLogDto
    {
        public int SystemId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public GetErrorLogDto()
        {
            
        }

        public GetErrorLogDto(int systemId, int pageSize, int pageNumber)
        {
            SystemId = systemId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
