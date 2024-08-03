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


        [ForeignKey("perInfo")]
        public int perInfoId { get; set; }
        public PersonalInformation? perInfo { get; set; }


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