using Microsoft.AspNetCore.Identity;

namespace Silownia_WebApi.Models
{
    public class User : IdentityUser
    {
        

        public ICollection<Reservation> reservations { get; set; }
        public ICollection<UserMembership> userMemberships { get; set; }
    }
}
