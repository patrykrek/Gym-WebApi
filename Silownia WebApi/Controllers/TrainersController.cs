using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Services;

namespace Silownia_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainersService _trainersService;

        public TrainersController(ITrainersService trainersService)
        {
            _trainersService = trainersService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("trainers")]
        public async Task<ActionResult> GetTrainers()
        {
            return Ok(await _trainersService.GetAllTrainersAsync());
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTrainer(int id)
        {
            return Ok(await _trainersService.GetTrainerAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("trainer")]
        public async Task<ActionResult> AddTrainer(AddTrainersDTO trainerDTO)
        {
            return Ok(await _trainersService.AddTrainersAsync(trainerDTO));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateTrainer(int id, UpdateTrainersDTO updateTrainer)
        {
            return Ok(await _trainersService.UpdateTrainersAsync(id, updateTrainer));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("trainer/{id}")]
        public async Task<ActionResult> DeleteTrainer(int id)
        {
            return Ok(await _trainersService.DeleteTrainersAsync(id));
        }
    }
}
