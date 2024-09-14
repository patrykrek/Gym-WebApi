namespace Silownia_WebApi.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerMonth { get; set; }

        public ICollection<User> users { get; set; }
        public ICollection<UserMembership> userMemberships { get; set; }
    }
}
