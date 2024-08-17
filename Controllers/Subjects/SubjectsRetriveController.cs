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
    public class SubjectsRetriveController : ControllerBase
    {
        private readonly ISubjectsRetrive subjectsret;

        public SubjectsRetriveController(ISubjectsRetrive SubjectsRet)
        {
            subjectsret = SubjectsRet;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await subjectsret.GetSubjectsAsync();
            return Ok(subjects);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSubjectDetail(int id)
        {
            var subjectDetail = await subjectsret.GetSubjectDetailAsync(id);
            if (subjectDetail == null)
            {
                return NotFound();
            }

            return Ok(subjectDetail);
        }

        [HttpDelete("Delete/{subjectId}")]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            bool result = await subjectsret.DeleteSubjectAsync(subjectId);

            if (!result)
            {
                return NotFound(new { Message = "Subject not found." });
            }

            return Ok(new { Message = "Subject deleted successfully." });
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddNewSubject([FromBody] AddNewSubject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSubject = await subjectsret.AddNewSubject(subject);
            return CreatedAtAction(nameof(GetSubjectDetail), new { id = newSubject.Id }, newSubject);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] UpdateSubject updateSubject)
        {
            if (id != updateSubject.Id)
            {
                return BadRequest("Subject ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await subjectsret.UpdateSubject(updateSubject);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}