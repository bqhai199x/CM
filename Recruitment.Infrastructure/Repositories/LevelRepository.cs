using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;
using System.Data;

namespace Recruitment.Infrastructure.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly IConfiguration _config;

        public LevelRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<int> AddAsync(Level level)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Insert Into Level (LevelName) " +
                    "Values (@LevelName)";

                return await connection.ExecuteAsync(sql, new
                {
                    LevelName = level.Name
                });
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteManyAsync(List<int> idList)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Delete From Level " +
                    $"Where LevelId = @LevelId";

                List<DynamicParameters> parameters = new List<DynamicParameters>();
                DynamicParameters parameter = new DynamicParameters();
                foreach (var item in idList)
                {
                    parameter = new();
                    parameter.Add("@LevelId", item, DbType.Int32);
                    parameters.Add(parameter);
                }

                return connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<Level>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Level.LevelId As Id, Level.LevelName as Name, " +
                        "Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, Candidate.Address, " +
                        "Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status " +
                    "From Level " +
                    "Inner Join Candidate On Candidate.LevelId = Level.LevelId";
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

        public async Task<Level> GetByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Level.LevelId As Id, Level.LevelName as Name, " +
                        "Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, Candidate.Address, " +
                        "Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status " +
                    "From Level " +
                    "Inner Join Candidate On Candidate.LevelId = Level.LevelId " +
                    $"Where Level.LevelId = {id}";
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

                return levelList.FirstOrDefault() ?? new();
            }
        }

        public async Task<int> UpdateAsync(Level level)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Update Level " +
                    "Set LevelName = @LevelName " +
                    "Where LevelId = @LevelId";

                return await connection.ExecuteAsync(sql, new
                {
                    LevelId = level.Id,
                    PositionName = level.Name
                });
            }
        }
    }
}
