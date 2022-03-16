using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_accountrole")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("account")]
        public string NIK { get; set; }
        [ForeignKey("role")]
        public int RoleId { get; set; }

        //relasi
        public virtual Role role { get; set; }
        public virtual Account account { get; set; }
    }
}
