using Trial.DTO;
using TRIAL.Persistence.entity;

namespace TRIAL.Services
{
    public interface IProfileService
    {
        Task<UserProfile> GetUserProfileAsync(int userId);
        Task<string> UpdateUserProfileAsync(UserProfile userProfile);

    }
}

