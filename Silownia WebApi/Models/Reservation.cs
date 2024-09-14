namespace Silownia_WebApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public User User { get; set; }

        public int TrainingId { get; set; } 
        public Trainings Trainings { get; set; }
    }
}
