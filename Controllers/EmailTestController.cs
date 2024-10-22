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

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTestController : ControllerBase
    {
        private readonly IEmailTestService EmailTestService;
        public EmailTestController(IEmailTestService emailTestService)
        {
            EmailTestService = emailTestService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            await EmailTestService.SendEmail(emailRequest.To, emailRequest.Subject, emailRequest.Body);
            return Ok("Email sent successfully.");
        }

    }
}