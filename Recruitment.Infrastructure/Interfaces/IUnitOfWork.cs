namespace Recruitment.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        ICandidateRepository Candidate { get; }

        ILevelRepository Level { get; }

        IPositionRepository Position { get; }
    }
}
