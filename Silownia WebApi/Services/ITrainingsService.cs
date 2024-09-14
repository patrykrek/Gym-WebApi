using Silownia_WebApi.DTO;

namespace Silownia_WebApi.Services
{
    public interface ITrainingsService
    {
        Task<List<GetTrainingDTO>> GetAllTrainingsAsync();
        Task<GetTrainingDTO> GetSingleTrainingAsync(int id);
        Task<List<GetTrainingDTO>> AddTrainingsAsync(AddTrainingsDTO addTraining);
        Task<GetTrainingDTO> UpdateTrainingsAsync(int id, UpdateTrainingDTO updateTraining);
        Task<List<GetTrainingDTO>> DeleteTrainingsAsync(int id);
    }
}
