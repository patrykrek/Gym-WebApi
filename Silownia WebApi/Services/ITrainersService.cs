using Silownia_WebApi.DTO;

namespace Silownia_WebApi.Services
{
    public interface ITrainersService
    {
        Task<List<GetTrainersDTO>> GetAllTrainersAsync();
        Task<GetTrainersDTO> GetTrainerAsync(int id);
        Task<List<GetTrainersDTO>> AddTrainersAsync(AddTrainersDTO trainerDTO);
        Task<List<GetTrainersDTO>> DeleteTrainersAsync(int id);
        Task<GetTrainersDTO> UpdateTrainersAsync(int id, UpdateTrainersDTO updatedTrainer);

    }
}
