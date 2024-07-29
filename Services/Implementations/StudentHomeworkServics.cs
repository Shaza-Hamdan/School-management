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
        private readonly AppDBContext _appDbContext;

        public StudentHomeworkService(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<StudentHomeworkDTO> SubmitHomeworkAsync(AddStudentHomeworkDTO addStudentHomeworkDto)
        {
            var studentHomework = new HomeworkS
            {
                perInfoId = addStudentHomeworkDto.perInfoId,
                homeworkTId = addStudentHomeworkDto.homeworkTId,
                Solution = addStudentHomeworkDto.Solution
            };

            _appDbContext.HwS.Add(studentHomework);
            await _appDbContext.SaveChangesAsync();

            return new StudentHomeworkDTO(studentHomework.Id, studentHomework.perInfoId, studentHomework.homeworkTId, studentHomework.Solution);
        }

        public async Task<StudentHomeworkDTO> GetStudentHomeworkByIdAsync(int id)
        {
            var studentHomework = await _appDbContext.HwS
                .Include(sh => sh.perInfo)
                .Include(sh => sh.homeworkT)
                .FirstOrDefaultAsync(sh => sh.Id == id);

            if (studentHomework == null)
            {
                return null;
            }

            return new StudentHomeworkDTO(studentHomework.Id, studentHomework.perInfoId, studentHomework.homeworkTId, studentHomework.Solution);
        }

        public async Task<bool> UpdateStudentHomeworkAsync(ModifyStudentHomeworkDTO modifyStudentHomeworkDto)
        {
            var studentHomework = await _appDbContext.HwS.FindAsync(modifyStudentHomeworkDto.Id);
            if (studentHomework == null)
            {
                return false;
            }

            studentHomework.Solution = modifyStudentHomeworkDto.Solution;

            _appDbContext.HwS.Update(studentHomework);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<StudentHomeworkDTO>> GetStudentHomeworkAsync()
        {
            return await _appDbContext.HwS
                .Select(h => new StudentHomeworkDTO(h.Id, h.perInfoId, h.homeworkTId, h.Solution))
                .ToListAsync();
        }
    }
}