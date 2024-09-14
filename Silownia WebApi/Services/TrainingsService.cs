using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Silownia_WebApi.Data;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Exceptions;
using Silownia_WebApi.Models;

namespace Silownia_WebApi.Services
{
    public class TrainingsService : ITrainingsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private List<Trainings> trainings = new List<Trainings>();

        public TrainingsService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTrainingDTO>> GetAllTrainingsAsync()
        {
            var gettrainings = await _context.Trainings.Select(t => _mapper.Map<GetTrainingDTO>(t)).ToListAsync();

            return gettrainings;
        }

        public async Task<GetTrainingDTO> GetSingleTrainingAsync(int id)
        {
            var findtraining = await _context.Trainings.FirstOrDefaultAsync(t => t.Id == id);

            if (findtraining == null)
            {
                throw new TrainingWithIdNotFound();
            }

            var training = _mapper.Map<GetTrainingDTO>(findtraining);

            return training;
        }
        public async Task<List<GetTrainingDTO>> AddTrainingsAsync(AddTrainingsDTO addTraining)
        {
            var training = new Trainings
            {
                Name = addTraining.Name,
                Description = addTraining.Description,
                StartDate = addTraining.StartDate,
                Duration = addTraining.Duration,
                TrainerId = addTraining.TrainerId,
            };

            await _context.Trainings.AddAsync(training);

            trainings.Add(training);

            await _context.SaveChangesAsync();

            var gettrainings = trainings.Select(t => _mapper.Map<GetTrainingDTO>(t)).ToList();

            return gettrainings;


        }

        public async Task<List<GetTrainingDTO>> DeleteTrainingsAsync(int id)
        {
            var findtraining = await _context.Trainings.FindAsync(id);

            if (findtraining == null)
            {
                throw new TrainingWithIdNotFound();
            }

            trainings.Remove(findtraining);

            _context.Trainings.Remove(findtraining);

            await _context.SaveChangesAsync();

            var gettrainings = trainings.Select(t => _mapper.Map<GetTrainingDTO>(t)).ToList();

            return gettrainings;

        }
        public async Task<GetTrainingDTO> UpdateTrainingsAsync(int id, UpdateTrainingDTO updateTraining)
        {
            var findtraining = await _context.Trainings.FindAsync(id);

            if (findtraining == null)
            {
                throw new TrainingWithIdNotFound();
            }

            findtraining.Name = updateTraining.Name;
            findtraining.Description = updateTraining.Description;
            findtraining.StartDate = updateTraining.StartDate;
            findtraining.Duration = updateTraining.Duration;

            await _context.SaveChangesAsync();

            var gettraining = _mapper.Map<GetTrainingDTO>(findtraining);

            return gettraining;
        }
    }
}
