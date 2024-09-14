namespace Silownia_WebApi.DTO
{
    public class AddTrainingsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public int TrainerId { get; set; }  
    }
}
