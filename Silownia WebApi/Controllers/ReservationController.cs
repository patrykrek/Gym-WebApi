using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silownia_WebApi.Services;
using System.Security.Claims;

namespace Silownia_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("reservations")]
        public async Task<ActionResult> GetReservations()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (user == null)
            {
                return Unauthorized();
            }

            if (User.IsInRole("Admin"))
            {
                return Ok(await _reservationService.GetAllReservationsAsync());
            }

            return Ok(await _reservationService.GetUserReservationAsync(user));
        }

        [Authorize(Roles = "User")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation(int TrainingId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(await _reservationService.CreateReservationAsync(TrainingId, user));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteReservation(int ReservationId)
        {
            return Ok(await _reservationService.DeleteReservationAsync(ReservationId));
        }
    }
}
