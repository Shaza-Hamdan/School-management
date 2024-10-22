using Microsoft.AspNetCore.Mvc;
using Trial.DTO;

namespace TRIAL.Services
{
    public interface IRegistrationService
    {
        string Register(string email);
        string Login(LoginRequest account);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordRequest model);
        string VerifyCode(VerifyCodeRequest request);

    }
}