using Silownia_WebApi.DTO;

namespace Silownia_WebApi.Services
{
    public interface IReservationService
    {
        Task<List<GetReservationDTO>> GetAllReservationsAsync();
        Task<List<GetReservationDTO>> GetUserReservationAsync(string UserId);
        Task<List<GetReservationDTO>> CreateReservationAsync(int TrainingId, string UserId);
        Task<List<GetReservationDTO>> DeleteReservationAsync(int ReservationId);
    }
}
