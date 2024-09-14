namespace Silownia_WebApi.Models
{
    public class Trainers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio {  get; set; }

        public ICollection<Trainings> trainings { get; set; }


    }
}
