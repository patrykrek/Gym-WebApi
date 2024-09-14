using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Silownia_WebApi.Data;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Exceptions;
using Silownia_WebApi.Models;
using System.Reflection.Metadata.Ecma335;

namespace Silownia_WebApi.Services
{
    public class TrainersService : ITrainersService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private List<Trainers> trainers = new List<Trainers>();

        public TrainersService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<GetTrainersDTO>> GetAllTrainersAsync()
        {
            var getdbtrainers = await _dataContext.Trainers.ToListAsync();

            if (getdbtrainers.Count == 0)
            {
                throw new TrainersNotFound();
            }

            var gettrainers = getdbtrainers.Select(t => _mapper.Map<GetTrainersDTO>(t)).ToList();

            return gettrainers;

            
        }

        public async Task<GetTrainersDTO> GetTrainerAsync(int id)
        {
            var findtrainer = await _dataContext.Trainers.FirstOrDefaultAsync(t => t.Id == id);

            if (findtrainer == null)
            {
                throw new TrainerWithIdNotFound();
            }

            var gettrainer = _mapper.Map<GetTrainersDTO>(findtrainer);

            return gettrainer;


        }
        public async Task<List<GetTrainersDTO>> AddTrainersAsync(AddTrainersDTO trainerDTO)
        {
            var trainer = new Trainers
            {
                Name = trainerDTO.Name,
                Bio = trainerDTO.Bio,
            };

            trainers.Add(trainer);

            await _dataContext.Trainers.AddAsync(trainer);

            await _dataContext.SaveChangesAsync();

            var gettrainer = trainers.Select(t => _mapper.Map<GetTrainersDTO>(t)).ToList();

            return gettrainer;

        }

        public async Task<List<GetTrainersDTO>> DeleteTrainersAsync(int id)
        {
            var findtrainer = await _dataContext.Trainers.FindAsync(id);

            if (findtrainer == null)
            {
                throw new TrainerWithIdNotFound();
            }

            trainers.Remove(findtrainer);

            _dataContext.Remove(findtrainer);

            await _dataContext.SaveChangesAsync();

            var gettrainers = trainers.Select(t => _mapper.Map<GetTrainersDTO>(t)).ToList();

            return gettrainers;

        }

        public async Task<GetTrainersDTO> UpdateTrainersAsync(int id, UpdateTrainersDTO updateTrainer)
        {
            var findtrainertoupdate = await _dataContext.Trainers.FindAsync(id);

            if(findtrainertoupdate == null)
            {
                throw new TrainerWithIdNotFound();

            }

            findtrainertoupdate.Name = updateTrainer.Name;
            findtrainertoupdate.Bio = updateTrainer.Bio;

            await _dataContext.SaveChangesAsync();

            var trainer = _mapper.Map<GetTrainersDTO>(findtrainertoupdate);

            return trainer;
           
        }

       
    }
}
