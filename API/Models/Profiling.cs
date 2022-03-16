using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_profiling")]
    public class Profiling
    {
        //Fk NIK
        [Key]
        [ForeignKey("Account")]
        public string NIK { get; set; }

        [Required]
        [ForeignKey("Education")]
        public int Education_id { get; set; }

        //Relation
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
    }
}