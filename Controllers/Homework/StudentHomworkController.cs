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
using TRIAL.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentHomeworkController : ControllerBase
    {
        private readonly IStudentHomeworkService studenthomeworkService;
        private readonly AppDBContext appdbContext;
        public StudentHomeworkController(AppDBContext appDbContext, IStudentHomeworkService studentHomeworkservice)
        {

            studenthomeworkService = studentHomeworkservice;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadHomework(int homeworkTId, int registrationId, IFormFile file)
        {
            try
            {
                var result = await studenthomeworkService.UploadHomeworkFileAsync(homeworkTId, registrationId, file);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Get/{RegistrationId}")]
        public async Task<IActionResult> GetStudentHomeworkById(int RegistrationId)
        {
            try
            {
                var homework = await studenthomeworkService.GetStudentHomeworkByIdAsync(RegistrationId);
                if (homework == null)
                {
                    return NotFound("Homework not found.");
                }

                // Optionally, return the file itself if you want the teacher to download it
                var filePath = homework.FilePath;
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);//Storing the file in a byte array
                return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));

                // Alternatively, you can return homework details if a download is not needed
                // return Ok(homework);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateHomework(int homeworkTId, int registrationId, IFormFile newFile)
        {
            if (newFile == null || newFile.Length == 0)
            {
                return BadRequest("A valid file must be uploaded.");
            }

            try
            {
                var result = await studenthomeworkService.UpdateHomeworkFileAsync(homeworkTId, registrationId, newFile);
                return Ok(new { message = result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // File not uploaded
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Record not found
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the homework file.");
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHomework(int Id)
        {
            bool result = await studenthomeworkService.DeleteHomework(Id);

            if (!result)
            {
                return NotFound(new { Message = "Homework has not been found or you do not have permission to delete it." });
            }

            return Ok(new { Message = "Homework deleted successfully." });
        }
    }
}