using LoggingManagerCore.Dtos;
using LoggingManagerCore.Entities;

namespace LoggingManagerCore.Ports.Primary
{
    public interface ISystemService
    {
        GenericResponse<List<EnrollSystem>>? getAll();
    }
}
