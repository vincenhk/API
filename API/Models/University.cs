using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }
}