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
using Microsoft.AspNetCore.Http.HttpResults;

namespace TRIAL.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        //database connection
        private readonly AppDBContext appdbContext;
        private readonly DatabaseSettings dBSettings;
        private readonly IEmailService emailservice; // Declare the email service

        public RegistrationService(AppDBContext appDbContext, IOptions<DatabaseSettings> dbSettings, IEmailService emailService)
        {
            appdbContext = appDbContext;
            dBSettings = dbSettings.Value ?? throw new ArgumentNullException(nameof(dbSettings));
            emailservice = emailService ?? throw new ArgumentNullException(nameof(emailService)); // Initialize the email service

        }

        //

        public string Register(CreateNewAccount account)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password); //Using BCrypt for hashing the password
            try
            {
                var registration = new Registration
                {
                    UserName = account.UserName,
                    Email = account.Email,
                    PasswordHash = passwordHash,
                    DateOfBirth = account.DateOfBirth,
                    Address = account.Address,
                    PhoneNumber = account.PhoneNumber
                };

                appdbContext.registrations.Add(registration);
                int rowsAffected = appdbContext.SaveChanges();

                if (rowsAffected > 0)
                {
                    return "Data Inserted";
                }
                else
                {
                    return "Error";
                }
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (ex) here or handle it as needed
                return $"SQL Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here or handle it as needed
                return $"General Error: {ex.Message}";
            }

        }



        public string Login(LoginRequest account)
        {
            var user = appdbContext.registrations.SingleOrDefault(u => u.Email == account.Email);

            if (user == null)
            {
                // User not found
                return "NotFound";
            }

            // Compare the hashed password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(account.Password, user.PasswordHash);
            return isPasswordValid ? "Login Successful" : "Invalid Email or Password";
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email) //send the link to the email after this the email contains the link
        {
            var user = await appdbContext.registrations.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return "User not found.";
            }

            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.ResetTokenExpiration = DateTime.Now.AddHours(1);
            appdbContext.registrations.Update(user); //This updates the user's record with the new token and expiration time.
            await appdbContext.SaveChangesAsync();

            var resetLink = $"http://localhost:5000/api/account/resetpassword?token={token}&email={email}";
            emailservice.SendEmail(email, "Password Reset Request", $"Click <a href='{resetLink}'>here</a> to reset your password.");

            return "Password reset token generated.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest model) //excuted after the user will click on the link that has been sent to him
        {
            var user = await appdbContext.registrations.FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordResetToken == model.Token);
            if (user == null || user.ResetTokenExpiration < DateTime.Now)
            {
                return "Invalid token or token expired.";
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiration = null;
            appdbContext.registrations.Update(user);
            await appdbContext.SaveChangesAsync();

            return "Password reset successful.";
        }


    }
}