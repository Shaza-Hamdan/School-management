using Microsoft.AspNetCore.Mvc;
using Trial.DTO;
using TRIAL.Persistence.entity;

public interface IHomeworkTeacherService
{
    Task<IEnumerable<HomeworkTDTO>> GetHomeworksAsync();
    Task<HomeworkTDTO> GetHomeworkByIdAsync(int id);
    Task<HomeworkTDTO> AddHomeworkAsync(AddHomeworkTDTO addHomeworkDto);
    Task<bool> UpdateHomeworkAsync(ModifyHomeworkTDTO modifyHomeworkDto);
    Task CleanupExpiredHomeworksAsync();

}