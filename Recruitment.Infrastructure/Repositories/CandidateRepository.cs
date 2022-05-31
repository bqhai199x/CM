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

        public Task<int> AddAsync(Candidate candidate)
        {
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Insert Into Candidate (PositionId, LevelId, FullName, Birthday, Address, Phone, Email, CVPath, IntroduceName, Status) " +
                    $"Values (@PositionId, @LevelId, @FullName, @BirthDay, @Address, @Phone, @Email, @CVPath, @IntroduceName, @Status)";

                return connection.ExecuteAsync(sql, new
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
            using (var connection = new MySqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                string sql =
                    "Delete From Candidate " +
                    $"Where CandidateId = {id}";

                return connection.ExecuteAsync(sql);
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

        public Task<int> UpdateAsync(Candidate entity)
        {
            throw new NotImplementedException();
        }
    }
}
