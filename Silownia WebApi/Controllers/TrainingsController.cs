using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Services;

namespace Silownia_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingsService _trainingService;
        public TrainingsController(ITrainingsService trainingService)
        {
            _trainingService = trainingService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("trainings")]
        public async Task<ActionResult> GetAllTrainings()
        {
            return Ok(await _trainingService.GetAllTrainingsAsync());
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleTraining(int id)
        {
            return Ok(await _trainingService.GetSingleTrainingAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> AddTraining(AddTrainingsDTO addTraining)
        {
            return Ok(await _trainingService.AddTrainingsAsync(addTraining));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateTrainings(int id, UpdateTrainingDTO updateTraining)
        {
            return Ok(await _trainingService.UpdateTrainingsAsync(id, updateTraining));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("remove")]
        public async Task<ActionResult> DeleteTrainings(int id)
        {
            return Ok(await _trainingService.DeleteTrainingsAsync(id));
        }
    }
}
