using TRIAL.Persistence.Repository;
using Trial.DTO;
using TRIAL.Services;
using TRIAL.Persistence.entity;
using Microsoft.EntityFrameworkCore;

namespace AssigningRoleU
{
    public class AssigningRole
    {
        private readonly AppDBContext appdbContext;
        public AssigningRole(AppDBContext appdBContext)
        {
            appdbContext = appdBContext;
        }

        public async Task<string> AssignRoleAsync(AssignRoleRequest request, string adminEmail)
        {
            //Validate the request
            if (string.IsNullOrWhiteSpace(request.UserEmail) || string.IsNullOrWhiteSpace(request.NewRole))
            {
                return "Invalid request parameters.";
            }

            //Find the user
            var user = await appdbContext.registrations.SingleOrDefaultAsync(u => u.Email == request.UserEmail);

            if (user == null)
            {
                return "User not found.";
            }

            //Check if the admin is authorized
            if (!await IsAdmin(adminEmail))
            {
                return "Not authorized";
            }

            //Assign the new role to the user
            user.Role = request.NewRole;

            //Save changes to the database
            appdbContext.registrations.Update(user);
            await appdbContext.SaveChangesAsync();

            return $"Role {request.NewRole} assigned to {user.Email} successfully.";
        }

        public async Task<bool> IsAdmin(string email)
        {
            var user = await appdbContext.registrations.SingleOrDefaultAsync(u => u.Email == email);
            return user != null && user.Role == "Admin"; // Assuming "Admin" is the role identifier
        }

        public async Task<bool> CreateInitialAdmin(string username, string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var newAdmin = new Registration(username, email, passwordHash, "Admin");

            // Check if any user exists already to prevent overwriting
            var existingUser = await appdbContext.registrations.SingleOrDefaultAsync(u => u.Email == email);
            if (existingUser != null) return false;

            appdbContext.registrations.Add(newAdmin);
            await appdbContext.SaveChangesAsync();

            return true;
        }
    }
}