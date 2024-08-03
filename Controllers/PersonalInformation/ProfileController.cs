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
        private readonly IProfileService profileservice;

        public ProfileController(IProfileService profileService)
        {
            profileservice = profileService;
        }

        [HttpGet("Get/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var userProfile = await profileservice.GetUserProfileAsync(userId);
            if (userProfile == null)
            {
                return NotFound("User not found.");
            }
            return Ok(userProfile);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfile userProfileDto)
        {
            var result = await profileservice.UpdateUserProfileAsync(userProfileDto);
            if (result == "User not found.")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}