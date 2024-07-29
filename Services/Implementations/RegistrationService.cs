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

namespace TRIAL.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        //database connection
        private readonly AppDBContext _appDbContext;
        private readonly DatabaseSettings _dbSettings;
        private readonly IEmailService _emailService; // Declare the email service

        public RegistrationService(AppDBContext appDbContext, IOptions<DatabaseSettings> dbSettings, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _dbSettings = dbSettings.Value ?? throw new ArgumentNullException(nameof(dbSettings));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService)); // Initialize the email service

        }

        //

        public string Register(CreateNewAccount account)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password); //Using BCrypt for hashing the password
            using (SqlConnection con = new SqlConnection(_dbSettings.MyConnection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO registrations (UserName, Email, PasswordHash,DateOfBirth,Address,PhoneNumber) VALUES (@user, @emailaddress, @PasswordHash,@dateOfBirth,@address,@phoneNumber)", con))
                {
                    cmd.Parameters.AddWithValue("@user", account.UserName);
                    cmd.Parameters.AddWithValue("@emailaddress", account.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@dateOfBirth", account.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Address", account.Address);
                    cmd.Parameters.AddWithValue("@phoneNumber", account.PhoneNumber);
                    // cmd.ExecuteNonQuery();
                    int rowsAffected = cmd.ExecuteNonQuery(); //cmd.ExecuteNonQuery(); => Excution
                    con.Close();
                    if (rowsAffected > 0)
                    {
                        return "Data Inserted";
                    }
                    else
                    {
                        return "Error";
                    }
                }

            }

        }

        public string Login(LoginRequest account)
        {
            using (SqlConnection con = new SqlConnection(_dbSettings.MyConnection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT PasswordHash FROM registrations WHERE Email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", account.Email);

                    var result = cmd.ExecuteScalar(); //The ExecuteScalar() method is used to execute the command and retrieve a single value (the password hash).
                    if (result != null)
                    {
                        string storedHash = result.ToString();
                        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(account.Password, storedHash);

                        return isPasswordValid ? "Login Successful" : "Invalid Email or Password";
                    }
                    else
                    {
                        return "Invalid Email or Password";
                    }
                }
            }

        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email) //send the link to the email after this the email contains the link
        {
            var user = await _appDbContext.registrations.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return "User not found.";
            }

            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.ResetTokenExpiration = DateTime.Now.AddHours(1);
            _appDbContext.registrations.Update(user); //This updates the user's record with the new token and expiration time.
            await _appDbContext.SaveChangesAsync();

            var resetLink = $"http://localhost:5000/api/account/resetpassword?token={token}&email={email}";
            _emailService.SendEmail(email, "Password Reset Request", $"Click <a href='{resetLink}'>here</a> to reset your password.");

            return "Password reset token generated.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest model) //excuted after the user will click on the link that has been sent to him
        {
            var user = await _appDbContext.registrations.FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordResetToken == model.Token);
            if (user == null || user.ResetTokenExpiration < DateTime.Now)
            {
                return "Invalid token or token expired.";
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiration = null;
            _appDbContext.registrations.Update(user);
            await _appDbContext.SaveChangesAsync();

            return "Password reset successful.";
        }


    }
}