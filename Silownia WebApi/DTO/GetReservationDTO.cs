using Silownia_WebApi.Models;

namespace Silownia_WebApi.DTO
{
    public class GetReservationDTO
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public int TrainingId { get; set; }
    }
}
