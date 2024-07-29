using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using School.Persistence.entity;

namespace TRIAL.Persistence.entity
{
    [Table("Person_Information")] // the name in db
    public class PerInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String? Name { get; set; }

        [Required]
        public String? Email { get; set; }

        [Required]
        public String? Address { get; set; }

        [Required]
        public decimal PersonalNum { get; set; }

        [Required]
        public String? Person { get; set; }

        //oto, every teacher has a subject
        public Subjects? subjects { get; set; }

        //parent mtm
        public ICollection<Marks>? subject { get; set; }

        public ICollection<HomeworkS>? homeworkTs { get; set; }

    }
}