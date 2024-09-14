namespace Silownia_WebApi.Exceptions
{
    public class MembershipNotFoundException : Exception
    {
        public MembershipNotFoundException() : base("You don't have any memberships.") { }
       
    }
}
