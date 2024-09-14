namespace Silownia_WebApi.Exceptions
{
    public class TrainerWithIdNotFound : Exception
    {
        public TrainerWithIdNotFound() : base("Trainer not found") { }
        
    }
}
