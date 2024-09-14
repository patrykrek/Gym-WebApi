using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silownia_WebApi.Models;
using Silownia_WebApi.Services;



namespace Silownia_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("userroles")]
        public async Task<ActionResult> GetUserRoles(string Email)
        {
            return Ok(await _roleService.GetUserRolesAsync(Email));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("newrole")]
        public async Task<ActionResult> AddRoles(string[] roles)
        {
            return Ok(await _roleService.AddRolesAsync(roles));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddUserRoles(UserRole user)
        {
            return Ok(await _roleService.AddUserRolesAsync(user.Email, user.Roles));
        }
    }
}
