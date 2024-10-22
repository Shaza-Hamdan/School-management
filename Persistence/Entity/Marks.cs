using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TRIAL.Persistence.entity
{
    public class Marks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [ForeignKey("Registration")]
        public int RegistrationId { get; set; }
        public Registration Registration { get; set; }


        [ForeignKey("subjects")]
        public int subjectsId { get; set; }
        public Subjects subjects { get; set; }


        [Required]
        [Column("Oral Mark")]
        public int Oral { get; set; }

        [Required]
        [Column("Written Mark")]
        public int Written { get; set; }

    }
}