
using Microsoft.EntityFrameworkCore;
using Trial.DTO;
using TRIAL.Persistence.Repository;
using TRIAL.Persistence.entity;

public class HomeworkTeacherService : IHomeworkTeacherService
{
    private readonly AppDBContext appdbContext;

    public HomeworkTeacherService(AppDBContext appDbContext)
    {
        appdbContext = appDbContext;
    }

    public async Task<IEnumerable<HomeworkTDTO>> GetHomeworksAsync()
    {
        await CleanupExpiredHomeworksAsync(); // Clean up expired homework before returning the list 
        return await appdbContext.HwT
            .Select(h => new HomeworkTDTO(h.Id, h.Homework, h.Discription, h.Deadline))
            .ToListAsync();
    }

    public async Task<HomeworkTDTO> GetHomeworkByIdAsync(int id)
    {
        var hw = await CleanupExpiredHomeworksAsync();
        if (hw == true) { }
        var homework = await appdbContext.HwT.FindAsync(id);
        if (homework == null)
        {
            return null;
        }
        return new HomeworkTDTO(homework.Id, homework.Homework, homework.Discription, homework.Deadline);
    }

    public async Task<string> AddHomeworkAsync(AddHomeworkTDTO addHomeworkDto)
    {
        var subject = await appdbContext.subjectNa.FindAsync(addHomeworkDto.subjectsId);
        if (subject == null)
        {
            return "null";
        }

        var homework = new HomeworkTeacher
        {
            Homework = addHomeworkDto.Homework,
            Discription = addHomeworkDto.Discription,
            Deadline = addHomeworkDto.Deadline,
            subjectsId = addHomeworkDto.subjectsId
        };

        appdbContext.HwT.Add(homework);
        await appdbContext.SaveChangesAsync();

        return "Homework added successfully";
    }

    public async Task<bool> UpdateHomeworkAsync(ModifyHomeworkTDTO modifyHomeworkDto)
    {
        var homework = await appdbContext.HwT.FindAsync(modifyHomeworkDto.Id);
        if (homework == null)
        {
            return false;
        }

        homework.Homework = modifyHomeworkDto.Homework;
        homework.Discription = modifyHomeworkDto.Discription;
        homework.Deadline = modifyHomeworkDto.Deadline;

        appdbContext.HwT.Update(homework);
        await appdbContext.SaveChangesAsync();

        return true;
    }
    public async Task<bool> CleanupExpiredHomeworksAsync()
    {
        var now = DateTime.UtcNow;
        var expiredHomeworks = appdbContext.HwT
            .Where(h => h.Deadline < now)
            .ToList();

        if (expiredHomeworks.Any()) //The Any() method returns "true" if the list contains at least one item and "false" if it is empty.
        {
            appdbContext.HwT.RemoveRange(expiredHomeworks);// method takes a collection and marks each entity in that collection for deletion in the context of the database.
            await appdbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

}
