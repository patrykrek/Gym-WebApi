namespace Silownia_WebApi.Models
{
    public class Trainings
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public ICollection<Reservation> reservations { get; set; }
        public int TrainerId { get; set; }
        public Trainers Trainer { get; set; }
    }
}
