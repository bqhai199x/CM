using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;
using System.Data;

namespace Recruitment.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IConfiguration _config;

        public PositionRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<int> AddAsync(Position position)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Insert Into Position (PositionName) " +
                    "Values (@PositionName)";

                return await connection.ExecuteAsync(sql, new
                {
                    PositionName = position.Name
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
                    "Delete From Position " +
                    $"Where PositionId = @PositionId";

                List<DynamicParameters> parameters = new List<DynamicParameters>();
                DynamicParameters parameter = new DynamicParameters();
                foreach (var item in idList)
                {
                    parameter = new();
                    parameter.Add("@PositionId", item, DbType.Int32);
                    parameters.Add(parameter);
                }

                return connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<Position>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Position.PositionId As Id, Position.PositionName as Name, " +
                        "Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, Candidate.Address, " +
                        "Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status " +
                    "From Position " +
                    "Inner Join Candidate On Candidate.LevelId = Position.PositionId";
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

        public async Task<Position> GetByIdAsync(int id)
        {
            using(var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Position.PositionId As Id, Position.PositionName as Name, " +
                        "Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, Candidate.Address, " +
                        "Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status " +
                    "From Position " +
                    "Inner Join Candidate On Candidate.LevelId = Position.PositionId " +
                    $"Where Position.PositionId = {id}";

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

                return positionList.FirstOrDefault() ?? new();
            }
        }

        public async Task<int> UpdateAsync(Position position)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Update Position " +
                    "Set PositionName = @PositionName " +
                    "Where PositionId = @PositionId";

                return await connection.ExecuteAsync(sql, new
                {
                    PositionId = position.Id,
                    PositionName = position.Name
                });
            }
        }
    }
}
