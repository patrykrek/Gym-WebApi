using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Silownia_WebApi.Data;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Exceptions;
using Silownia_WebApi.Models;

namespace Silownia_WebApi.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private List<Membership> memberships = new List<Membership>();

        public MembershipService(DataContext datacontext, IMapper mapper)
        {
            _dataContext = datacontext;
            _mapper = mapper;
        }

        public async Task<List<GetMembershipDTO>> GetAllMembershipsAsync() //user can get all the memberships which he can buy
        {
            var getDbMembership = await _dataContext.Memberships.ToListAsync();

            var getMembership = getDbMembership.Select(m => _mapper.Map<GetMembershipDTO>(m)).ToList();

            return getMembership;
        }

        public async Task<List<GetUserMembershipDTO>> GetAllUsersMembershipsAsync() // for admin
        {
            var getDbUsersMemberships = await _dataContext.UserMemberships.ToListAsync();

            var getMemberships = getDbUsersMemberships.Select(um => _mapper.Map<GetUserMembershipDTO>(um)).ToList();

            return getMemberships;
        } 

        public async Task<List<GetUserMembershipDTO>> GetUserMembershipsAsync(string userId)
        {
            var getDbUserMembership = await _dataContext.UserMemberships.Where(m => m.userId == userId).ToListAsync();

            if (!getDbUserMembership.Any())
            {
                throw new MembershipNotFoundException();
            }

            var getUserMembership = getDbUserMembership.Select(m => _mapper.Map<GetUserMembershipDTO>(m)).ToList(); 

            return getUserMembership;
        }

        public async Task<GetMembershipDTO> GetSingleMembershipAsync(int id)
        {
            var findmembership = await _dataContext.Memberships.FindAsync(id);

            if (findmembership == null)
            {
                throw new MembershipNotFoundException();

            }

            var getMembership = _mapper.Map<GetMembershipDTO>(findmembership);

            return getMembership;

        }

        public async Task<int> BuyMembershipAsync(int membershipId, string userId)
        {
            var findmembership = await _dataContext.Memberships.FindAsync(membershipId);

            if (findmembership == null)
            {
                throw new MembershipNotFoundException();
            }

            var newUserMembership = new UserMembership
            {
                userId = userId,
                MembershipId = membershipId
            };

            await _dataContext.AddAsync(newUserMembership);
          
            await _dataContext.SaveChangesAsync();

            return findmembership.Id;

            
        }

        public async Task<List<GetMembershipDTO>> AddMembershipAsync(AddMembershipDTO addMembership)
        {
            var checkMembershipName = await _dataContext.Memberships.FirstOrDefaultAsync(m => m.Name == addMembership.Name);

            if (checkMembershipName != null)
            {
                throw new MembershipAlreadyExist();
            }
            var membership = new Membership
            {
                Name = addMembership.Name,
                PricePerMonth = addMembership.PricePerMonth,
            };

            memberships.Add(membership);

            await _dataContext.Memberships.AddAsync(membership);

            await _dataContext.SaveChangesAsync();

            var getMembership = memberships.Select(m => _mapper.Map<GetMembershipDTO>(m)).ToList();

            return getMembership;


        }

        public async Task<List<GetMembershipDTO>> DeleteMembershipAsync(int id)
        {
            var findMembership = await _dataContext.Memberships.FindAsync(id);

            if (findMembership == null)
            {
                throw new MembershipNotFoundException();
            }

            memberships.Remove(findMembership);

            _dataContext.Memberships.Remove(findMembership);

            await _dataContext.SaveChangesAsync();

            var getMemberships = memberships.Select(m => _mapper.Map<GetMembershipDTO>(m)).ToList();

            return getMemberships;
        }     
        public async Task<GetMembershipDTO> UpdateMembershipAsync(int id, UpdateMembershipDTO updateMembership)
        {
            var findMembership = await _dataContext.Memberships.FindAsync(id);

            if (findMembership == null)
            {
                throw new MembershipNotFoundException();
            }

            findMembership.Name = updateMembership.Name;
            findMembership.PricePerMonth = updateMembership.PricePerMonth;

            await _dataContext.SaveChangesAsync();

            var getMembership = _mapper.Map<GetMembershipDTO>(findMembership);

            return getMembership;
        }
    }
}
