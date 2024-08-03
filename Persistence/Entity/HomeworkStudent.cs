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
        public string? Solution { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;


        [ForeignKey("perInfo")]
        public int perInfoId { get; set; }
        public PersonalInformation? perInfo { get; set; }


        [ForeignKey("homeworkT")]
        public int homeworkTId { get; set; }
        public HomeworkTeacher? homeworkT { get; set; }
    }
}