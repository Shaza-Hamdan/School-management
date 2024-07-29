using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.DTO;

[ApiController]
[Route("api/[controller]")]
public class HomeworkTController : ControllerBase
{
    //ljjvkv
    private readonly IHomeworkTService _homeworkService;

    public HomeworkTController(IHomeworkTService homeworkService)
    {
        _homeworkService = homeworkService;
    }

    [HttpGet]
    public async Task<IEnumerable<HomeworkTDTO>> GetHomeworks()
    {
        return await _homeworkService.GetHomeworksAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HomeworkTDTO>> GetHomework(int id)
    {
        var homework = await _homeworkService.GetHomeworkByIdAsync(id);
        if (homework == null)
        {
            return NotFound();
        }
        return homework;
    }

    [HttpPost]
    public async Task<ActionResult<HomeworkTDTO>> AddHomework(AddHomeworkTDTO addHomeworkDto)
    {
        var homework = await _homeworkService.AddHomeworkAsync(addHomeworkDto);
        return CreatedAtAction(nameof(GetHomework), new { id = homework.Id }, homework);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHomework(int id, ModifyHomeworkTDTO modifyHomeworkDto)
    {
        if (id != modifyHomeworkDto.Id)
        {
            return BadRequest();
        }

        var result = await _homeworkService.UpdateHomeworkAsync(modifyHomeworkDto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
