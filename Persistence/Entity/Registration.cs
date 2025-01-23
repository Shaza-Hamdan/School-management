using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TRIAL.Persistence.entity
{
    public partial class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid E-mail address.")] //validate the input as an Email Address
        [Required]
        public string Email { get; set; }
        //[Required]
        // public string Role { get; set; }  // Roles: "Admin", "Teacher", "Student", etc.

        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        // for reset account's password
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
        //

        //public bool IsProfileComplete { get; set; } = false;

        // Constructor
        // public Registration() { }
        // public Registration(string username, string email, string passwordHash, string role)
        // {
        //     UserName = username;
        //     Email = email;
        //     PasswordHash = passwordHash;
        //     Role = role;
        // }

        //oto, every teacher has a subject
        public Subjects subjects { get; set; }

        //parent mtm
        public ICollection<Marks> subject { get; set; }

        public ICollection<HomeworkStudent> homeworkTs { get; set; }
    }

}