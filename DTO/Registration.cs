using System.ComponentModel.DataAnnotations;

namespace Trial.DTO
{
    public record EmailRequest(
        string To,
        string Subject,
        string Body

    );

    public record AssignRoleRequest(
        string UserEmail, // The ID of the user to whom the role will be assigned
        string NewRole // The new role to be assigned (e.g., "Teacher", "Student")
    );

    public record CreateAdminRequest(
        string UserEmail,
        string Password,
        string UserName

    );
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

    // public record UserProfile
    // (
    //  int Id,
    //  string UserName,
    //  string Email,
    //  DateTime? DateOfBirth,
    //  string Address,
    //  string PhoneNumber
    //  );


}
