using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TRIAL.Persistence.entity;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Trial.DTO;
using TRIAL.Services;
using Microsoft.Extensions.Logging;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var userProfile = await _profileService.GetUserProfileAsync(userId);
            if (userProfile == null)
            {
                return NotFound("User not found.");
            }
            return Ok(userProfile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfile userProfileDto)
        {
            var result = await _profileService.UpdateUserProfileAsync(userProfileDto);
            if (result == "User not found.")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}