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
    public class SubjectsRetrive : ISubjectsRetrive
    {
        private readonly AppDBContext appdbContext;
        public SubjectsRetrive(AppDBContext appDbContext)
        {
            appdbContext = appDbContext;
        }

        public async Task<IEnumerable<SubjectRetrieving>> GetSubjectsAsync() //this method returns a collection  of lists 
        {
            var sub = await appdbContext.subjectNa.ToListAsync();
            return sub.Select(subs => new SubjectRetrieving(
                        subs.SubName,
                        subs.Discription
                    ));
        }
        public async Task<SubjectDetails> GetSubjectDetailAsync(int subjectId)
        {
            var subject = await appdbContext.subjectNa
                .Include(s => s.marks)
                .Include(s => s.homeworkTs)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                return null;
            }

            var marks = subject.marks.Select(m => new MarkDTO(m.Id, m.Oral, m.Written));//Convert Marks to MarkDTOs 
            var homeworkAssignments = subject.homeworkTs.Select(h => new HomeworkDTO(h.Id, h.Homework, h.Deadline, h.Description));//Convert Homework Assignments to HomeworkDTOs

            return new SubjectDetails(subject.Id, subject.SubName, marks, homeworkAssignments);
        }

        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            var subject = await appdbContext.subjectNa
           .Include(s => s.marks)
           .Include(s => s.homeworkTs)
           .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
            {
                return false;
            }

            if (subject.marks != null && subject.marks.Any())
            {
                appdbContext.marks.RemoveRange(subject.marks);
            }

            if (subject.homeworkTs != null && subject.homeworkTs.Any())
            {
                appdbContext.HwT.RemoveRange(subject.homeworkTs);
            }

            appdbContext.subjectNa.Remove(subject);
            await appdbContext.SaveChangesAsync();
            return true;
        }
    }

}





