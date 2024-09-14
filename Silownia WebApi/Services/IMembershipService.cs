

using Silownia_WebApi.DTO;

namespace Silownia_WebApi.Services
{
    public interface IMembershipService
    {
        Task<List<GetMembershipDTO>> GetAllMembershipsAsync();
        Task<List<GetUserMembershipDTO>> GetAllUsersMembershipsAsync();
        Task<List<GetUserMembershipDTO>> GetUserMembershipsAsync(string userId);
        Task<int> BuyMembershipAsync(int membershipId, string userId);
        Task<GetMembershipDTO> GetSingleMembershipAsync(int id);
        Task<List<GetMembershipDTO>> AddMembershipAsync(AddMembershipDTO addMembership);
        Task<GetMembershipDTO> UpdateMembershipAsync(int id, UpdateMembershipDTO updateMembership);
        Task<List<GetMembershipDTO>> DeleteMembershipAsync(int id);
    }
}
