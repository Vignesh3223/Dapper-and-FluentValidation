using DapperDemo.Dto;
using DapperDemo.Entities;

namespace DapperDemo.Contract
{
    public interface ITraineeRepository
    {
        public Task<IEnumerable<Trainee>> GetTrainees();
        public Task<Trainee> GetTraineeById(int id);
        public Task<Trainee> CreateTrainee(TraineeDto trainee);
        public Task UpdateTrainee(int id, TraineeDto trainee);
        public Task DeleteTrainee(int id);

    }
}
