namespace Recruitment.Entities
{
    public class Candidate
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public Level Level { get; set; } = new();

        public Position Position { get; set; } = new();

        public DateTime? Birthday { get; set; } = null;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string CVPath { get; set; } = string.Empty;

        public string Introduce { get; set; } = string.Empty;

        public StatusValue Status { get; set; }

        public enum StatusValue
        {

        }
    }
}
