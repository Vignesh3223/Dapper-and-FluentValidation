using DapperDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DapperDemo.Context
{
    public class TraineeContext:DbContext
    {
        public TraineeContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Trainee> Trainees { get; set; }
    }
}