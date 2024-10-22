using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.DTO;

[ApiController]
[Route("api/teacher/[controller]")]
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
    public async Task<ActionResult<HomeworkTDTO>> AddHomework(AddHomeworkTDTO addHomeworkDto)
    {
        var homework = await homeworkservice.AddHomeworkAsync(addHomeworkDto);
        return CreatedAtAction(nameof(GetHomework), new { id = homework.Id }, homework);
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
        await homeworkservice.CleanupExpiredHomeworksAsync();
        return NoContent(); // Returns a 204 No Content response
    }
}
