using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRIAL.Persistence.entity
{
    public class HomeworkStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "TEXT")] // For large text data
        public string Solution { get; set; }

        [Column(TypeName = "DATETIME")] // For date and time
        public DateTime Created { get; set; } // = DateTime.Now;

        [ForeignKey("Registration")]
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; }

        [ForeignKey("HomeworkT")]
        public int HomeworkTId { get; set; }
        public HomeworkTeacher HomeworkT { get; set; }
    }
}
