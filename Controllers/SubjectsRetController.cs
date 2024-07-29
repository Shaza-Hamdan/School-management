using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TRIAL.Persistence.entity;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Trial.DTO;
using TRIAL.Services;
using Microsoft.Extensions.Logging;

namespace TRIAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsRetController : ControllerBase
    {
        private readonly ISubjectsRet _subjectsRet;

        public SubjectsRetController(ISubjectsRet SubjectsRet)
        {
            _subjectsRet = SubjectsRet;
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _subjectsRet.GetSubjectsAsync();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectDetail(int id)
        {
            var subjectDetail = await _subjectsRet.GetSubjectDetailAsync(id);
            if (subjectDetail == null)
            {
                return NotFound();
            }

            return Ok(subjectDetail);
        }
    }
}