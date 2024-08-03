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
        private readonly IStudentHomeworkService studenthomeworkService;

        public StudentHomeworkController(IStudentHomeworkService studentHomeworkservice)
        {
            studenthomeworkService = studentHomeworkservice;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> SubmitHomework([FromBody] AddStudentHomeworkDTO addStudentHomeworkDto)
        {
            var result = await studenthomeworkService.SubmitHomeworkAsync(addStudentHomeworkDto);
            return CreatedAtAction(nameof(GetStudentHomeworkById), new { id = result.Id }, result);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetStudentHomeworkById(int id)
        {
            var result = await studenthomeworkService.GetStudentHomeworkByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateStudentHomework([FromBody] ModifyStudentHomeworkDTO modifyStudentHomeworkDto)
        {
            var result = await studenthomeworkService.UpdateStudentHomeworkAsync(modifyStudentHomeworkDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetStudentHomeworkAsync()
        {
            var homeworks = await studenthomeworkService.GetStudentHomeworkAsync();
            return Ok(homeworks);
        }

        [HttpDelete("{deleteStudentHomeworkId}")]
        public async Task<IActionResult> DeleteHomework(DeleteHomework hwork)
        {
            bool result = await studenthomeworkService.DeleteHomework(hwork);

            if (!result)
            {
                return NotFound(new { Message = "Homework submission not found or you do not have permission to delete it." });
            }

            return Ok(new { Message = "Homework submission deleted successfully." });
        }
    }
}