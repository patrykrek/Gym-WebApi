using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Silownia_WebApi.Data;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Exceptions;
using Silownia_WebApi.Models;

namespace Silownia_WebApi.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private List<Reservation> reservations = new List<Reservation>();

        public ReservationService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<List<GetReservationDTO>> GetAllReservationsAsync()
        {
            var getDbReser = await _dataContext.Reservations.ToListAsync();

            var getReser = getDbReser.Select(r => _mapper.Map<GetReservationDTO>(r)).ToList();

            return getReser;
        }
        public async Task<List<GetReservationDTO>> GetUserReservationAsync(string UserId)
        {
            var getDbUserReser = await _dataContext.Reservations.Where(r => r.UserId == UserId).ToListAsync();

            if (!getDbUserReser.Any())
            {
                throw new ReservationNotFoundException();
            }

            var getUserReser = getDbUserReser.Select(r => _mapper.Map<GetReservationDTO>(r)).ToList();

            return getUserReser;
        }
        public async Task<List<GetReservationDTO>> CreateReservationAsync(int TrainingId, string UserId)
        {
            
            var findtraining = await _dataContext.Trainings.FindAsync(TrainingId);

            if (findtraining == null)
            {
                throw new TrainingWithIdNotFound();
            }

            var findUserMembership = await _dataContext.UserMemberships.FirstOrDefaultAsync(m => m.userId == UserId);

            if (findUserMembership == null)
            {
                throw new MembershipNotFoundException();
            }

            var reservation = new Reservation
            {
                ReservationDate = DateTime.Now,
                TrainingId = TrainingId,
                UserId = UserId

            };

            reservations.Add(reservation);

            await _dataContext.Reservations.AddAsync(reservation);

            await _dataContext.SaveChangesAsync();

            var getReser = reservations.Select(r => _mapper.Map<GetReservationDTO>(r)).ToList();

            return getReser;

        }

        public async Task<List<GetReservationDTO>> DeleteReservationAsync(int ReservationId)
        {
            var findreser = await _dataContext.Reservations.FindAsync(ReservationId);

            if (findreser == null)
            {
                throw new ReservationNotFoundException();
            }

            _dataContext.Reservations.Remove(findreser);

            reservations.Remove(findreser);

            await _dataContext.SaveChangesAsync();

            var getReser = reservations.Select(r => _mapper.Map<GetReservationDTO>(r)).ToList();

            return getReser;
        }

        
    }
}
