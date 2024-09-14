namespace Silownia_WebApi.Exceptions
{
    public class ReservationNotFoundException : Exception
    {
        public ReservationNotFoundException() : base("Reservation not found") { }
       
    }
}
