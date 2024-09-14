namespace Silownia_WebApi.Models
{
    public class UserMembership
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }

    }
}
