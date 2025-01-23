using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.DTO;

[ApiController]
[Route("api/[controller]")]
public class HomeworkTeacherController : ControllerBase
{
    private readonly IHomeworkTeacherService homeworkservice;

    public HomeworkTeacherController(IHomeworkTeacherService homeworkService)
    {
        homeworkservice = homeworkService;
    }

    [HttpGet("Get")]
    public async Task<IEnumerable<HomeworkTDTO>> GetHomeworks()
    {
        return await homeworkservice.GetHomeworksAsync();
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<HomeworkTDTO>> GetHomework(int id)
    {
        var homework = await homeworkservice.GetHomeworkByIdAsync(id);
        if (homework == null)
        {
            return NotFound();
        }
        return homework;
    }

    [HttpPost("Post")]
    public async Task<string> AddHomework(AddHomeworkTDTO addHomeworkDto)
    {

        // Call the service method to add the homework
        var homework = await homeworkservice.AddHomeworkAsync(addHomeworkDto);

        // Check if the homework creation was successful
        if (homework == null)
        {
            return "Homework could not be created.";
        }
        else
        {
            return "Homework added successfully";
        }

    }

    [HttpPut("Put/{id}")]
    public async Task<IActionResult> UpdateHomework(int id, ModifyHomeworkTDTO modifyHomeworkDto)
    {
        if (id != modifyHomeworkDto.Id)
        {
            return BadRequest();
        }

        var result = await homeworkservice.UpdateHomeworkAsync(modifyHomeworkDto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Endpoint to clean up expired homeworks
    [HttpPost("cleanup-expired")]
    public async Task<IActionResult> CleanupExpiredHomeworks()
    {
        var deleted = await homeworkservice.CleanupExpiredHomeworksAsync();
        if (deleted)
        {
            return Ok("Expired homework has been deleted.");
        }
        else
        {
            return Ok("No expired homework has been found.");
        }
    }
}
