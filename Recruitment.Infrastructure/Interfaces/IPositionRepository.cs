using Recruitment.Core.Entities;

namespace Recruitment.Infrastructure.Interfaces
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        Task<int> DeleteManyAsync(List<int> idList);
    }
}
