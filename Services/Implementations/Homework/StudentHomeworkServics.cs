
using Trial.DTO;
using TRIAL.Persistence.entity;
using TRIAL.Persistence.Repository;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace TRIAL.Services.Implementations
{
    public class StudentHomeworkService : IStudentHomeworkService
    {
        private readonly AppDBContext appdbContext;

        public StudentHomeworkService(AppDBContext appDbContext)
        {
            appdbContext = appDbContext;
        }

        public async Task<StudentHomeworkDTO> SubmitHomeworkAsync(AddStudentHomeworkDTO addStudentHomeworkDto)
        {
            var studentHomework = new HomeworkStudent
            {
                RegistrationId = addStudentHomeworkDto.RegistrationId,
                HomeworkTId = addStudentHomeworkDto.homeworkTId,
                Solution = addStudentHomeworkDto.Solution
            };

            appdbContext.HwS.Add(studentHomework);
            await appdbContext.SaveChangesAsync();

            return new StudentHomeworkDTO(studentHomework.Id, studentHomework.RegistrationId, studentHomework.HomeworkTId, studentHomework.Solution);
        }

        public async Task<StudentHomeworkDTO> GetStudentHomeworkByIdAsync(int id)
        {
            var studentHomework = await appdbContext.HwS
                .Include(sh => sh.Registration)
                .Include(sh => sh.HomeworkT)
                .FirstOrDefaultAsync(sh => sh.Id == id);

            if (studentHomework == null)
            {
                return null;
            }

            return new StudentHomeworkDTO(studentHomework.Id, studentHomework.RegistrationId, studentHomework.HomeworkTId, studentHomework.Solution);
        }

        public async Task<bool> UpdateStudentHomeworkAsync(ModifyStudentHomeworkDTO modifyStudentHomeworkDto)
        {
            var studentHomework = await appdbContext.HwS.FindAsync(modifyStudentHomeworkDto.Id);
            if (studentHomework == null)
            {
                return false;
            }

            studentHomework.Solution = modifyStudentHomeworkDto.Solution;

            appdbContext.HwS.Update(studentHomework);
            await appdbContext.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<StudentHomeworkDTO>> GetStudentHomeworkAsync()
        {
            return await appdbContext.HwS
                .Select(h => new StudentHomeworkDTO(h.Id, h.RegistrationId, h.HomeworkTId, h.Solution))
                .ToListAsync();
        }

        public async Task<bool> DeleteHomework(DeleteHomework hwork)
        {
            // Find the homework submission based on its ID and the student's ID
            var studentHomework = await appdbContext.HwS
                .FirstOrDefaultAsync(h => h.HomeworkTId == hwork.homeworkTId && h.RegistrationId == hwork.RegistrationId);

            if (studentHomework == null)
            {
                // Homework submission not found
                return false;
            }

            // Remove the homework submission from the context
            appdbContext.HwS.Remove(studentHomework);
            await appdbContext.SaveChangesAsync();

            return true;
        }



    }
}