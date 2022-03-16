using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string GPA { get; set; }

        [Required]
        [ForeignKey("university")]
        public int University_Id { get; set; }

        //relation
        [JsonIgnore]
        public virtual  ICollection<Profiling> profilings { get; set; }
        [JsonIgnore]
        public virtual University university { get; set; }
    }
}