using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //relasi
        public virtual ICollection<AccountRole> accountRole { get; set; }
    }
}
