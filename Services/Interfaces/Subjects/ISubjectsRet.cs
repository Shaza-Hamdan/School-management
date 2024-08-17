using Trial.DTO;
using TRIAL.Persistence.entity;

namespace TRIAL.Services
{
    public interface ISubjectsRetrive
    {
        Task<IEnumerable<SubjectRetrieving>> GetSubjectsAsync();
        Task<SubjectDetails> GetSubjectDetailAsync(int subjectId);
        Task<bool> DeleteSubjectAsync(int subjectId);
        Task<AddNewSubjectDTO> AddNewSubject(AddNewSubject subject);
        Task<bool> UpdateSubject(UpdateSubject UpSub);
    }
}