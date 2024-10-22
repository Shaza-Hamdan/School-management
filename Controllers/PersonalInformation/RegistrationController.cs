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
using Microsoft.AspNetCore.Authorization;
using AssigningRoleU;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationservice;
        private readonly AssigningRole assigningRole;
        // private readonly EmailService emailService;
        public RegistrationController(IRegistrationService registrationService, AssigningRole assigningrole)//, EmailService emailservice)  //, ILogger<RegistrationController> logger)
        {
            registrationservice = registrationService;
            assigningRole = assigningrole;
            // emailService = emailservice;
        }

        [HttpPost("register")]
        public IActionResult Register(string email)
        {
            //registrationservice.Register(account);

            var result = registrationservice.Register(email);
            if (result == "Email already exists")
            {
                return BadRequest(new { message = "Email already exists." });
            }

            return Ok(new { message = "Verification code sent to email." });
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromForm] CreateAdminRequest request)
        {
            var result = await assigningRole.CreateInitialAdmin(request.UserEmail, request.UserEmail, request.Password);

            if (result)
            {
                return Redirect("/home"); // Redirect to the main application page
            }

            return BadRequest("Error creating admin. Please try again.");
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var result = await assigningRole.AssignRoleAsync(request, User.Identity.Name); //the system will automatically identify the admin's email using User.Identity.Name in the backend code, which retrieves the email of the currently authenticated admin making the request.
            if (result == "Not authorized")
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("VerifyCode")]
        public IActionResult VerifyCode([FromBody] VerifyCodeRequest request)
        {
            var result = registrationservice.VerifyCode(request);
            if (result == "Invalid or expired verification code")
            {
                return BadRequest(result); // 400 Bad Request
            }

            return Ok(result); // 200 OK
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = registrationservice.Login(account);
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
            var result = await registrationservice.GeneratePasswordResetTokenAsync(request.Email);
            if (result == "Password reset token generated.")
            {
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = result });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {
            var result = await registrationservice.ResetPasswordAsync(model);
            if (result == "Password reset successful.")
            {
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = result });
        }


    }
}