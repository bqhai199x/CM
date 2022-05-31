using Recruitment.Core.Entities;

namespace Recruitment.Infrastructure.Interfaces
{
    public interface ILevelRepository : IGenericRepository<Level>
    {
        Task<int> DeleteManyAsync(List<int> idList);
    }
}
