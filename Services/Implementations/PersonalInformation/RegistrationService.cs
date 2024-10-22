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
using EncryptDecrypt;
using VerificationRegisterN;

namespace TRIAL.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        //database connection
        private readonly AppDBContext appdbContext;
        private readonly DatabaseSettings dBSettings;
        private readonly IEmailTestService emailTestservice; // Declare the email service
        private readonly VerificationRegister verificationRegister;

        public RegistrationService(AppDBContext appDbContext, IOptions<DatabaseSettings> dbSettings, IEmailTestService EmailTestService, VerificationRegister verificationregister)
        {
            appdbContext = appDbContext;
            dBSettings = dbSettings.Value;
            emailTestservice = EmailTestService;
            verificationRegister = verificationregister;

        }


        public string Register(string email)
        {
            // Check if the email already exists
            var existingUser = appdbContext.registrations.SingleOrDefault(u => u.Email == email);

            if (existingUser != null)
            {
                return "Email already exists";
            }

            // Email does not exist, generate a verification code
            string verificationCode = verificationRegister.GenerateVerificationCode();

            // Store the verification code in a temporary table or cache with expiration
            verificationRegister.StoreVerificationCode(email, verificationCode);

            // Send verification code to the email
            emailTestservice.SendEmail(email, "Verification Code", $"Your verification code is {verificationCode}");
            return "Verification code sent to email";
        }

        public string VerifyCode(VerifyCodeRequest request)
        {
            var verificationEntry = appdbContext.emailVerification
                .SingleOrDefault(v => v.Email == request.Email && v.Code == request.Code);

            if (verificationEntry == null || verificationEntry.Expiry < DateTime.Now)
            {
                return "Invalid or expired verification code";
            }

            // Code is valid, proceed with sending the username and password
            return verificationRegister.GenerateAndSendCredentials(request.Email);
        }


        public string Login(LoginRequest account)
        {
            var user = appdbContext.registrations.SingleOrDefault(u => u.Email == account.Email);

            if (user == null)
            {
                return "NotFound";
            }

            string decryptedPassword = EnDePassword.ConvertToDecrypt(user.PasswordHash);

            // Compare the hashed password
            if (decryptedPassword != account.Password)
            {
                return "Invalid Email or Password";
            }
            // Check if the user's profile is complete
            if (!user.IsProfileComplete)
            {
                // If the profile data is provided in the login request, update the profile
                if (!string.IsNullOrEmpty(account.Address) &&
                    !string.IsNullOrEmpty(account.PhoneNumber) &&
                    account.DateOfBirth != default(DateTime))
                {
                    // Update user's profile information
                    user.Address = account.Address;
                    user.PhoneNumber = account.PhoneNumber;
                    user.DateOfBirth = account.DateOfBirth;
                    user.IsProfileComplete = true; // Mark profile as complete

                    appdbContext.SaveChanges();

                    return "Login Successful, Profile Updated";
                }
                else
                {
                    // Profile is incomplete and required data is missing
                    return "ProfileIncomplete, Fill the required fields";
                }
            }

            return "Login Successful";

        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await appdbContext.registrations.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return "User not found.";
            }

            var token = Guid.NewGuid().ToString(); //generates a Globally Unique IDentifier....128-bit number
            user.PasswordResetToken = token;
            user.ResetTokenExpiration = DateTime.Now.AddHours(1);
            appdbContext.registrations.Update(user); //This updates the user's record with the new token and expiration time.
            await appdbContext.SaveChangesAsync();

            var resetLink = $"http://localhost:5000/api/account/resetpassword?token={token}&email={email}";
            emailTestservice.SendEmail(email, "Password Reset Request", $"Click <a href='{resetLink}'>here</a> to reset your password.");//here is a hyperlink

            return "Password reset token generated.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest model) //excuted after the user will click on the link that has been sent to him
        {
            var user = await appdbContext.registrations.FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordResetToken == model.Token);
            if (user == null || user.ResetTokenExpiration < DateTime.Now)
            {
                return "Invalid token or token expired.";
            }

            user.PasswordHash = EnDePassword.ConvertToEncrypt(model.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiration = null;
            appdbContext.registrations.Update(user);
            await appdbContext.SaveChangesAsync();

            return "Password reset successful.";
        }


    }
}