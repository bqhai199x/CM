namespace Recruitment.Core.Entities
{
    public class Level
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public List<Candidate> CandidateList { get; set; } = new();
    }
}
