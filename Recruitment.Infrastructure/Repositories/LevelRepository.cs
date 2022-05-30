using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;

namespace Recruitment.Infrastructure.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly IConfiguration _config;

        public LevelRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task<int> AddAsync(Level entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Level>> GetAllAsync()
        {
            string sql =
                "Select Level.LevelId As Id, Level.LevelName as Name, " +
                    "Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, Candidate.Address, " +
                    "Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status " +
                "From Level " +
                "Inner Join Candidate On Candidate.LevelId = Level.LevelId";
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                var levelDict = new Dictionary<int, Level>();
                var levelList = await connection.QueryAsync<Level, Candidate, Level>(sql, (lv, can) =>
                {
                    if (!levelDict.TryGetValue(lv.Id, out var currentLv))
                    {
                        currentLv = lv;
                        levelDict.Add(currentLv.Id, currentLv);
                    }
                    currentLv.CandidateList.Add(can);
                    return currentLv;
                }, splitOn: "Id");
                return levelList.Distinct().ToList();
            }
        }

        public Task<Level> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Level entity)
        {
            throw new NotImplementedException();
        }
    }
}
