namespace Silownia_WebApi.DTO
{
    public class UpdateTrainingDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}
