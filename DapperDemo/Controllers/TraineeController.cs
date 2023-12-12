using AutoMapper;
using DapperDemo.Contract;
using DapperDemo.Dto;
using DapperDemo.Entities;
using DapperDemo.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TraineeController : ControllerBase
    {
        private readonly ITraineeRepository _repository;
        private readonly IMapper _mapper;
        public TraineeController(ITraineeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainees()
        {
            var trainees = await _repository.GetTrainees();
            var traineeData = _mapper.Map<List<TraineeViewDto>>(trainees);
            return Ok(traineeData);
        }

        [HttpGet("{id}", Name = "TraineeById")]
        public async Task<IActionResult> GetTraineeById(int id)
        {
            var trainee = await _repository.GetTraineeById(id);
            var singleTrainee = _mapper.Map<TraineeViewDto>(trainee);
            return Ok(singleTrainee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainee([FromBody] TraineeDto trainee)
        {
            TraineeValidator validator = new TraineeValidator();
            var validationResult = validator.Validate(trainee);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var createdTrainee = await _repository.CreateTrainee(trainee);
            return CreatedAtRoute("TraineeById", new { id = createdTrainee.Id }, createdTrainee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainee(int id, [FromBody] TraineeDto updtrainee)
        {
            var trainee = await _repository.GetTraineeById(id);
            if (trainee is null)
                return NotFound();

            TraineeValidator validator = new TraineeValidator();
            var validationResult = validator.Validate(updtrainee);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _repository.UpdateTrainee(id, updtrainee);

            return Ok(updtrainee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee(int id)
        {
            var trainee = await _repository.GetTraineeById(id);
            if (trainee is null)
                return NotFound();

            await _repository.DeleteTrainee(id);
            return Ok("Deleted Successfully");
        }
    }
}
