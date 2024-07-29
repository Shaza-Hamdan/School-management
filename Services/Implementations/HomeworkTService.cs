using System.Net;
using System.Net.Mail;
using TRIAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Trial.DTO;
using TRIAL.Persistence.Repository;
using TRIAL.Persistence.entity;

public class HomeworkTService : IHomeworkTService
{
    private readonly AppDBContext _appDbContext;

    public HomeworkTService(AppDBContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<HomeworkTDTO>> GetHomeworksAsync()
    {
        await CleanupExpiredHomeworksAsync(); // Clean up before returning the list
        return await _appDbContext.HwT
            .Select(h => new HomeworkTDTO(h.Id, h.Homework, h.Description, h.Deadline))
            .ToListAsync();
    }

    private async Task CleanupExpiredHomeworksAsync()
    {
        var now = DateTime.UtcNow;
        var expiredHomeworks = _appDbContext.HwT
            .Where(h => h.Deadline < now)
            .ToList();

        if (expiredHomeworks.Any())
        {
            _appDbContext.HwT.RemoveRange(expiredHomeworks);
            await _appDbContext.SaveChangesAsync();
        }
    }
    public async Task<HomeworkTDTO> GetHomeworkByIdAsync(int id)
    {
        var homework = await _appDbContext.HwT.FindAsync(id);
        if (homework == null)
        {
            return null;
        }
        return new HomeworkTDTO(homework.Id, homework.Homework, homework.Description, homework.Deadline);
    }

    public async Task<HomeworkTDTO> AddHomeworkAsync(AddHomeworkTDTO addHomeworkDto)
    {
        var subject = await _appDbContext.subjectNa.FindAsync(addHomeworkDto.subjectsId);
        if (subject == null)
        {
            throw new Exception("The specified subject does not exist.");
        }

        var homework = new HomeworkT
        {
            Homework = addHomeworkDto.Homework,
            Description = addHomeworkDto.Description,
            Deadline = addHomeworkDto.Deadline
        };

        _appDbContext.HwT.Add(homework);
        await _appDbContext.SaveChangesAsync();

        return new HomeworkTDTO(homework.Id, homework.Homework, homework.Description, homework.Deadline);
    }

    public async Task<bool> UpdateHomeworkAsync(ModifyHomeworkTDTO modifyHomeworkDto)
    {
        var homework = await _appDbContext.HwT.FindAsync(modifyHomeworkDto.Id);
        if (homework == null)
        {
            return false;
        }

        homework.Homework = modifyHomeworkDto.Homework;
        homework.Description = modifyHomeworkDto.Description;
        homework.Deadline = modifyHomeworkDto.Deadline;

        _appDbContext.HwT.Update(homework);
        await _appDbContext.SaveChangesAsync();

        return true;
    }
}
