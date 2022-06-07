using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Recruitment.Core.Entities;
using Recruitment.Infrastructure.Interfaces;
using System.Data;

namespace Recruitment.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IConfiguration _config;

        public CandidateRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<int> AddAsync(Candidate candidate)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Insert Into Candidate (PositionId, LevelId, FullName, Birthday, Address, Phone, Email, CVPath, IntroduceName, Status) " +
                    $"Values (@PositionId, @LevelId, @FullName, @BirthDay, @Address, @Phone, @Email, @CVPath, @IntroduceName, @Status)";

                return await connection.ExecuteAsync(sql, new
                {
                    PositionId = candidate.Position.Id,
                    LevelId = candidate.Level.Id,
                    FullName = candidate.Name,
                    Birthday = candidate.Birthday,
                    Address = candidate.Address,
                    Phone = candidate.Phone,
                    Email = candidate.Email,
                    CVPath = candidate.CVPath,
                    IntroduceName = candidate.Introduce,
                    Status = candidate.Status
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
                    "Delete From Candidate " +
                    $"Where CandidateId = @CandidateId";

                List<DynamicParameters> parameters = new List<DynamicParameters>();
                DynamicParameters parameter = new DynamicParameters();
                foreach (var item in idList)
                {
                    parameter = new();
                    parameter.Add("@CandidateId", item, DbType.Int32);
                    parameters.Add(parameter);
                }

                return connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<Candidate>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, " +
                        "Candidate.Address, Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status,  " +
                        "Level.LevelId As Id, Level.LevelName As Name, Position.PositionId As Id, Position.PositionName As Name " +
                    "From Candidate " +
                    "Inner Join Level On Candidate.LevelId = Level.LevelId " +
                    "Inner Join Position On Candidate.PositionId = Position.PositionId ";

                var candidateList = await connection.QueryAsync<Candidate, Level, Position, Candidate>(sql, (can, lv, pos) =>
                {
                    can.Level = lv;
                    can.Position = pos;
                    return can;
                }, splitOn: "Id, Id");

                return candidateList.ToList();
            }
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, " +
                        "Candidate.Address, Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status,  " +
                        "Level.LevelId As Id, Level.LevelName As Name, Position.PositionId As Id, Position.PositionName As Name " +
                    "From Candidate " +
                    "Inner Join Level On Candidate.LevelId = Level.LevelId " +
                    "Inner Join Position On Candidate.PositionId = Position.PositionId " +
                    $"Where Candidate.CandidateId = {id}";

                var candidateList = await connection.QueryAsync<Candidate, Level, Position, Candidate>(sql, (can, lv, pos) =>
                {
                    can.Level = lv;
                    can.Position = pos;
                    return can;
                }, splitOn: "Id, Id");

                return candidateList.FirstOrDefault() ?? new();
            }
        }

        public async Task<List<Candidate>> SearchAsync(Candidate.Condition condition)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Select Candidate.CandidateId As Id, Candidate.FullName As Name, Candidate.Birthday, " +
                        "Candidate.Address, Candidate.Phone, Candidate.Email, Candidate.CVPath, Candidate.IntroduceName As Introduce, Candidate.Status,  " +
                        "Level.LevelId As Id, Level.LevelName As Name, Position.PositionId As Id, Position.PositionName As Name " +
                    "From Candidate " +
                    "Inner Join Level On Candidate.LevelId = Level.LevelId " +
                    "Inner Join Position On Candidate.PositionId = Position.PositionId " +
                    "Where 1 = 1 ";

                if (condition.LevelId != 0)
                {
                    sql += "And Candidate.LevelId = @LevelId ";
                }
                if (condition.PositionId != 0)
                {
                    sql += "And Candidate.PositionId = @PositionId ";
                }
                if (!string.IsNullOrEmpty(condition.Name))
                {
                    sql += "And Candidate.FullName Like CONCAT('%', @Name, '%') ";
                }
                if (!string.IsNullOrEmpty(condition.Introduce))
                {
                    sql += "And Candidate.IntroduceName = @Introduce ";
                }
                if (condition.Status != null)
                {
                    sql += "And Candidate.Status = @Status ";
                }

                var candidateList = await connection.QueryAsync<Candidate, Level, Position, Candidate>(sql, (can, lv, pos) =>
                {
                    can.Level = lv;
                    can.Position = pos;
                    return can;
                }, splitOn: "Id, Id", param: new
                {
                    LevelId = condition.LevelId,
                    PositionId = condition.PositionId,
                    Name = condition.Name,
                    Introduce = condition.Introduce,
                    Status = condition.Status
                });

                return candidateList.ToList();
            }
        }

        public Task<int> UpdateAsync(Candidate candidate)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Update Candidate " +
                    "Set " +
                        "PositionId = @PositionId, " +
                        "LevelId = @LevelId, " +
                        "FullName = @FullName, " +
                        "Birthday = @Birthday, " +
                        "Address = @Address, " +
                        "Phone = @Phone, " +
                        "Email = @Email, " +
                        "CVPath = @CVPath, " +
                        "IntroduceName = @IntroduceName, " +
                        "Status = @Status " +
                    "Where CandidateId = @CandidateId";

                return connection.ExecuteAsync(sql, new
                {
                    CandidateId = candidate.Id,
                    PositionId = candidate.Position.Id,
                    LevelId = candidate.Level.Id,
                    FullName = candidate.Name,
                    Birthday = candidate.Birthday,
                    Address = candidate.Address,
                    Phone = candidate.Phone,
                    Email = candidate.Email,
                    CVPath = candidate.CVPath,
                    IntroduceName = candidate.Introduce,
                    Status = candidate.Status
                });
            }
        }
    }
}
