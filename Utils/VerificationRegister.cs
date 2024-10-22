
using TRIAL.Persistence.Repository;
using Trial.DTO;
using TRIAL.Services;
using TRIAL.Persistence.entity;
using EncryptDecrypt;

namespace VerificationRegisterN
{
    public class VerificationRegister
    {
        private readonly AppDBContext appdbContext;
        private readonly IEmailTestService emailTestService;

        public VerificationRegister(AppDBContext appdBContext, IEmailTestService EmailTestService)
        {
            appdbContext = appdBContext;
            emailTestService = EmailTestService;
        }
        public string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // 6-digit code //The Next() method of the Random class generates a random integer within a specified range
        }

        public void StoreVerificationCode(string email, string code)
        {
            // You can store this in a temporary table with expiration time
            var verificationEntry = new EmailVerification
            {
                Email = email,
                Code = code,
                Expiry = DateTime.Now.AddMinutes(15) // Set expiry time
            };

            appdbContext.emailVerification.Add(verificationEntry);
            appdbContext.SaveChanges();
        }

        public string GenerateAndSendCredentials(string email)
        {
            // Extract the local part of the email for the username
            string localPart = email.Split('@')[0]; // Get the part before '@'

            // Generate username and password
            string username = localPart + "_" + Guid.NewGuid().ToString().Substring(0, 4);
            string password = Guid.NewGuid().ToString().Substring(0, 10); // Random 10-character password

            // Hash the password before storing
            string passwordHash = EnDePassword.ConvertToEncrypt(password);

            // Store the user in the database
            // var registration = new Registration
            // {
            //     UserName = username,
            //     Email = email,
            //     PasswordHash = passwordHash

            // };

            var registration = new Registration(username, email, passwordHash, "Student");

            appdbContext.registrations.Add(registration);
            appdbContext.SaveChanges();

            // Send the username and password to the user's email
            emailTestService.SendEmail(email, "Your Account Details", $"Username: {username}\nPassword: {password}");

            return "Username and password sent to email";
        }
    }
}