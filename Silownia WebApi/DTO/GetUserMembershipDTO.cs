using Silownia_WebApi.Models;

namespace Silownia_WebApi.DTO
{
    public class GetUserMembershipDTO
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public int MembershipId { get; set; }

    }
}
