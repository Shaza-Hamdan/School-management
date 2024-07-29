using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRIAL.Persistence.entity
{
    public class HomeworkT
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Homework { get; set; }

        [Required]
        [Column("Discription")]
        public string Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }


        [ForeignKey("subjects")]
        public int subjectsId { get; set; }
        public Subjects? subjects { get; set; }

        public ICollection<HomeworkS>? perInfos { get; set; }
    }
}