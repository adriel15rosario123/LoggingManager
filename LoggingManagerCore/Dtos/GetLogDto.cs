using System.Text.Json.Serialization;

namespace LoggingManagerCore.Dtos
{
    public class GetLogDto
    {
        public int SystemId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public GetLogDto()
        {
            
        }

        public GetLogDto(int systemId, int pageSize, int pageNumber)
        {
            SystemId = systemId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
