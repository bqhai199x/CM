using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;

namespace Recruitment.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IConfiguration _config;

        public PositionRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task<int> AddAsync(Position entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Position>> GetAllAsync()
        {
            string sql =
                "Select Position.PositionId As Id, Position.PositionName as Name, " +
                    "Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, Candidate.Address, " +
                    "Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status " +
                "From Position " +
                "Inner Join Candidate On Candidate.LevelId = Position.PositionId";
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                var positionDict = new Dictionary<int, Position>();
                var positionList = await connection.QueryAsync<Position, Candidate, Position>(sql, (pos, can) =>
                {
                    if (!positionDict.TryGetValue(pos.Id, out var currentPos))
                    {
                        currentPos = pos;
                        positionDict.Add(currentPos.Id, currentPos);
                    }
                    currentPos.CandidateList.Add(can);
                    return currentPos;
                }, splitOn: "Id");
                return positionList.Distinct().ToList();
            }
        }

        public Task<Position> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Position entity)
        {
            throw new NotImplementedException();
        }
    }
}
