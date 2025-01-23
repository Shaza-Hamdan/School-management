using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRIAL.Persistence.entity
{
    [Table("subjectNa")]
    public class Subjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public String? SubName { get; set; }
        [Required]
        public String? Discription { get; set; }


        //oto, every teacher has a subject

        [ForeignKey("Registration")]
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; }


        //parent mtm
        public ICollection<Marks> marks { get; set; }

        //parent otm
        public ICollection<HomeworkTeacher> homeworkTs { get; set; }

    }
}