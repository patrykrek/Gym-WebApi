using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Silownia_WebApi.DTO;
using Silownia_WebApi.Services;
using System.Security.Claims;

namespace Silownia_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;
        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("memberships")]
        public async Task<ActionResult> GetAllMemberships()
        {
            return Ok(await _membershipService.GetAllMembershipsAsync());
        }


        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleMembership(int id)
        {
            return Ok(await _membershipService.GetSingleMembershipAsync(id));
        }

        [Authorize(Roles = "User")]
        [HttpPost("buy/{membershipId}")]
        public async Task<ActionResult> BuyMembership(int membershipId)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(await _membershipService.BuyMembershipAsync(membershipId,user));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("mymemberships")]
        public async Task<ActionResult> GetUserMemberships()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (user == null)
            {
                return Unauthorized();
            }

            if (User.IsInRole("Admin"))
            {
                return Ok(await _membershipService.GetAllUsersMembershipsAsync());
            }

            return Ok(await _membershipService.GetUserMembershipsAsync(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("newmembership")]
        public async Task<ActionResult> AddMembership(AddMembershipDTO membershipDTO)
        {
            return Ok(await _membershipService.AddMembershipAsync(membershipDTO));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateMembership(int id, UpdateMembershipDTO updateMembership)
        {
            return Ok(await _membershipService.UpdateMembershipAsync(id, updateMembership));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteMembership(int id)
        {
            return Ok(await _membershipService.DeleteMembershipAsync(id));
        }


    }
}
