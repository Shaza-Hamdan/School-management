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

        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        // Add these properties
        public string PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }

    }

}