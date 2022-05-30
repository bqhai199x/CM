using Recruitment.Infrastructure.Interfaces;

namespace Recruitment.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICandidateRepository candidateRepository, ILevelRepository levelRepository, IPositionRepository positionRepository)
        {
            Candidate = candidateRepository;
            Level = levelRepository;
            Position = positionRepository;
        }

        public ICandidateRepository Candidate { get; }

        public ILevelRepository Level { get; }

        public IPositionRepository Position { get; }
    }
}
