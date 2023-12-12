using AutoMapper;
using DapperDemo.Dto;
using DapperDemo.Entities;

namespace DapperDemo.Mappings
{
    public class TraineeProfile : Profile
    {
        public TraineeProfile()
        {
            CreateMap<Trainee, TraineeViewDto>();
        }
    }
}
