using System.Net;
using System;
using Trial.DTO;
using TRIAL.Persistence.entity;
using System.Collections.Generic;
using TRIAL.Persistence.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Cryptography;
using System.Text;
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
                PerInfoId = addStudentHomeworkDto.perInfoId,
                HomeworkTId = addStudentHomeworkDto.homeworkTId,
                Solution = addStudentHomeworkDto.Solution
            };

            appdbContext.HwS.Add(studentHomework);
            await appdbContext.SaveChangesAsync();

            return new StudentHomeworkDTO(studentHomework.Id, studentHomework.PerInfoId, studentHomework.HomeworkTId, studentHomework.Solution);
        }

        public async Task<bool> DeleteHomework(DeleteHomework hwork)
        {
            // Find the homework submission based on its ID and the student's ID
            var studentHomework = await appdbContext.HwS
                .FirstOrDefaultAsync(h => h.HomeworkTId == hwork.homeworkTId && h.PerInfoId == hwork.perInfoId);

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


        public async Task<StudentHomeworkDTO> GetStudentHomeworkByIdAsync(int id)
        {
            var studentHomework = await appdbContext.HwS
                .Include(sh => sh.PerInfo)
                .Include(sh => sh.HomeworkT)
                .FirstOrDefaultAsync(sh => sh.Id == id);

            if (studentHomework == null)
            {
                return null;
            }

            return new StudentHomeworkDTO(studentHomework.Id, studentHomework.PerInfoId, studentHomework.HomeworkTId, studentHomework.Solution);
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
                .Select(h => new StudentHomeworkDTO(h.Id, h.PerInfoId, h.HomeworkTId, h.Solution))
                .ToListAsync();
        }


    }
}