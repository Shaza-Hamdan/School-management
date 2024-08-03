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
    public class ProfileService : IProfileService

    {
        private readonly AppDBContext appdbContext;

        public ProfileService(AppDBContext appDbContext)
        {
            appdbContext = appDbContext;
        }

        public async Task<UserProfile> GetUserProfileAsync(int userId)
        {
            var user = await appdbContext.registrations.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new UserProfile
            (
                user.Id,
                user.UserName,
                user.Email,
                user.DateOfBirth,
                user.Address,
                user.PhoneNumber
            );
        }

        public async Task<string> UpdateUserProfileAsync(UserProfile userProfileDto)
        {
            var existingUser = await appdbContext.registrations.FindAsync(userProfileDto.Id);
            if (existingUser == null)
            {
                return "User not found.";
            }

            existingUser.UserName = userProfileDto.UserName;
            existingUser.DateOfBirth = userProfileDto.DateOfBirth;
            existingUser.Address = userProfileDto.Address;
            existingUser.PhoneNumber = userProfileDto.PhoneNumber;

            appdbContext.registrations.Update(existingUser);
            await appdbContext.SaveChangesAsync();

            return "Profile updated successfully.";
        }
    }
}