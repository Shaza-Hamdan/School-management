using Microsoft.AspNetCore.Mvc;
using Trial.DTO;

namespace TRIAL.Services
{
    public interface IEmailTestService
    {
        Task SendEmail(string to, string subject, string body);
    }
}