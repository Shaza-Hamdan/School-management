using Trial.DTO;
using TRIAL.Persistence.entity;

namespace TRIAL.Services
{
    public interface ISubjectsRet
    {
        Task<IEnumerable<SubjectRetrieving>> GetSubjectsAsync();
        Task<SubjectDetails> GetSubjectDetailAsync(int subjectId);
    }
}