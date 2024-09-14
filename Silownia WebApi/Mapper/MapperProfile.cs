using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Models;

namespace Silownia_WebApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddTrainingsDTO, Trainings>();
            CreateMap<Trainings, GetTrainingDTO>();
            CreateMap<UpdateTrainingDTO, Trainings>();
            CreateMap<Trainers, GetTrainersDTO>();
            CreateMap<Membership, GetMembershipDTO>();
            CreateMap<UpdateMembershipDTO, Membership>();
            CreateMap<UserMembership, GetUserMembershipDTO>();
            CreateMap<Reservation, GetReservationDTO>();

        }
    }
}
