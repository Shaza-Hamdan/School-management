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
    public class StudentHomeworkController : ControllerBase
    {
        private readonly IStudentHomeworkService _studentHomeworkService;

        public StudentHomeworkController(IStudentHomeworkService studentHomeworkService)
        {
            _studentHomeworkService = studentHomeworkService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitHomework([FromBody] AddStudentHomeworkDTO addStudentHomeworkDto)
        {
            var result = await _studentHomeworkService.SubmitHomeworkAsync(addStudentHomeworkDto);
            return CreatedAtAction(nameof(GetStudentHomeworkById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentHomeworkById(int id)
        {
            var result = await _studentHomeworkService.GetStudentHomeworkByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateStudentHomework([FromBody] ModifyStudentHomeworkDTO modifyStudentHomeworkDto)
        {
            var result = await _studentHomeworkService.UpdateStudentHomeworkAsync(modifyStudentHomeworkDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpGet("homeworks")]
        public async Task<IActionResult> GetStudentHomeworkAsync()
        {
            var homeworks = await _studentHomeworkService.GetStudentHomeworkAsync();
            return Ok(homeworks);
        }
    }
}