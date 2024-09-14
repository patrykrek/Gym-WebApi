using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silownia_WebApi.Models;

namespace Silownia_WebApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var getroles = await _roleManager.Roles.Select(r => new Role { Id = Guid.Parse(r.Id), Name = r.Name }).ToListAsync();

            return getroles;
        }

        public async Task<List<string>> GetUserRolesAsync(string Email)
        {
            var finduser = await _userManager.FindByEmailAsync(Email);

            if (finduser == null)
            {
                throw new Exception("User with that email doesn't exist");
            }

            var getuserroles = await _userManager.GetRolesAsync(finduser);

            return getuserroles.ToList();


        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var list = new List<string>();

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var newrole = await _roleManager.CreateAsync(new IdentityRole(role));

                    list.Add(role);
                }
            }
            return list;
        }

        public async Task<bool> AddUserRolesAsync(string Email, string[] roles)
        {
            var finduser = await _userManager.FindByEmailAsync(Email);

            if (finduser == null)
            {
                throw new Exception($"User with that email doesn't exist");
            }

            var roleexist = await RoleExists(roles);

            if (roleexist != null || roleexist.Count == roles.Length)
            {
                var assignRole = await _userManager.AddToRolesAsync(finduser, roleexist);

                return assignRole.Succeeded;
            }
            return false;

        }


        private async Task<List<string>> RoleExists(string[] roles)
        {
            var list = new List<string>();

            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    list.Add(role);
                }
            }
            return list;
        }

        

        
    }
}
