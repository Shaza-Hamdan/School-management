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
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult Register(CreateNewAccount account)
        {
            //_registrationService.Register(account);
            try
            {
                var result = _registrationService.Register(account);
                if (result == "Data Inserted")
                {
                    return Ok(result);
                }

                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _registrationService.Login(account);
            if (result == "Login Successful")
            {
                return Ok(result);
            }
            else if (result == "Invalid Email or Password")
            {
                return Unauthorized(result);
            }
            else
            {
                return StatusCode(500, result);
            }

        }

        [HttpPost("generate-password-reset-token")]
        public async Task<IActionResult> GeneratePasswordResetToken([FromBody] PasswordResetRequest request)
        {
            var result = await _registrationService.GeneratePasswordResetTokenAsync(request.Email);
            if (result == "Password reset token generated.")
            {
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = result });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var result = await _registrationService.ResetPasswordAsync(model);
            if (result == "Password reset successful.")
            {
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = result });
        }
    }
}