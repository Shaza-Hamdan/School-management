using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TRIAL.Persistence.entity
{
    public class HomeworkS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Solution { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;


        [ForeignKey("perInfo")]
        public int perInfoId { get; set; }
        public PerInfo? perInfo { get; set; }


        [ForeignKey("homeworkT")]
        public int homeworkTId { get; set; }
        public HomeworkT? homeworkT { get; set; }
    }
}