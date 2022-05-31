using Recruitment.Core.Entities;

namespace Recruitment.Infrastructure.Interfaces
{
    public interface ICandidateRepository : IGenericRepository<Candidate>
    {
        Task<List<Candidate>> SearchAsync(Candidate.Condition condition);

        Task<int> DeleteManyAsync(List<int> idList);
    }
}
