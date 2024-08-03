using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRIAL.Persistence.entity
{
    public class Subjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public String? SubName { get; set; }
        [Required]
        public String? Discription { get; set; }

        [ForeignKey("perInfo")]
        public int perInfoId { get; set; }
        public PersonalInformation perInfo { get; set; }

        //parent mtm
        public ICollection<Marks> marks { get; set; }

        //parent otm
        public ICollection<HomeworkTeacher> homeworkTs { get; set; }

    }
}