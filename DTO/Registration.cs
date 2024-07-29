using System.ComponentModel.DataAnnotations;

namespace Trial.DTO
{
    public record CreateNewAccount(
        string UserName,
        string Password,
        string Email,
        DateTime? DateOfBirth,
        string Address,
        string PhoneNumber
    );

    public record LoginRequest(
        string Email,
        string Password
    );

    public record PasswordResetRequest(
     string Email
    );
    public record ResetPasswordRequest
    (
        string Email,
        string Token,
        string NewPassword
    );

    public record UserProfile
    (
     int Id,
     string UserName,
     string Email,
     DateTime? DateOfBirth,
     string Address,
     string PhoneNumber
     );
}
