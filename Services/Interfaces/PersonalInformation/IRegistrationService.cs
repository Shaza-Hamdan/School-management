using Trial.DTO;

namespace TRIAL.Services
{
    public interface IRegistrationService
    {
        string Register(CreateNewAccount account);
        string Login(LoginRequest account);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordRequest model);
    }
}