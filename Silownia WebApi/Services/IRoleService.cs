using Silownia_WebApi.Models;

namespace Silownia_WebApi.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<List<string>> GetUserRolesAsync(string Email);
        Task<List<string>> AddRolesAsync(string[] roles);
        Task<bool> AddUserRolesAsync(string Email, string[] roles);

    }
}
