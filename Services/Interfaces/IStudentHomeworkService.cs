using Trial.DTO;

namespace TRIAL.Services
{
    public interface IStudentHomeworkService
    {
        Task<StudentHomeworkDTO> SubmitHomeworkAsync(AddStudentHomeworkDTO addStudentHomeworkDto);
        Task<StudentHomeworkDTO> GetStudentHomeworkByIdAsync(int id);
        Task<IEnumerable<StudentHomeworkDTO>> GetStudentHomeworkAsync();
        Task<bool> UpdateStudentHomeworkAsync(ModifyStudentHomeworkDTO modifyStudentHomeworkDto);
    }
}