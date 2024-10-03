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

        [ForeignKey("PerInfo")] // Updated to PascalCase
        public int PerInfoId { get; set; } // Updated to PascalCase
        public PersonalInformation PerInfo { get; set; }

        [ForeignKey("HomeworkT")] // Updated to PascalCase
        public int HomeworkTId { get; set; } // Updated to PascalCase
        public HomeworkTeacher HomeworkT { get; set; }
    }
}
