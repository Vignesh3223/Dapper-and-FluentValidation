using Dapper;
using DapperDemo.Context;
using DapperDemo.Contract;
using DapperDemo.Dto;
using DapperDemo.Entities;
using System.Data;

#nullable disable
namespace DapperDemo.Repository
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly DapperContext _context;

        public TraineeRepository(DapperContext context) => _context = context;

        public async Task<Trainee> CreateTrainee(TraineeDto trainee)
        {
            var query = "INSERT INTO Trainees (FirstName, LastName, Email, City) values (@FirstName, @LastName, @Email, @City)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", trainee.FirstName, DbType.String);
            parameters.Add("LastName", trainee.LastName, DbType.String);
            parameters.Add("Email", trainee.Email, DbType.String);
            parameters.Add("City", trainee.City, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdTrainee = new Trainee
                {
                    Id = id,
                    FirstName = trainee.FirstName,
                    LastName = trainee.LastName,
                    Email = trainee.Email,
                    City = trainee.City
                };
                return createdTrainee;
            }
        }

        public async Task DeleteTrainee(int id)
        {
            var query = "DELETE FROM Trainees WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Trainee> GetTraineeById(int id)
        {
            var query = "SELECT * FROM Trainees WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var trainee = await connection.QuerySingleOrDefaultAsync<Trainee>(query, new { id });
                return trainee;
            }
        }

        public async Task<IEnumerable<Trainee>> GetTrainees()
        {
            var query = "SELECT * FROM Trainees";

            using (var connection = _context.CreateConnection())
            {
                var trainees = await connection.QueryAsync<Trainee>(query);
                return trainees.ToList();
            }
        }

        public async Task UpdateTrainee(int id, TraineeDto trainee)
        {
            var query = "UPDATE Trainees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, City = @City WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FirstName", trainee.FirstName, DbType.String);
            parameters.Add("LastName", trainee.LastName, DbType.String);
            parameters.Add("Email", trainee.Email, DbType.String);
            parameters.Add("City", trainee.City, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
