using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_account")]
    public class Account
    {
        [Key]
        [ForeignKey("Employees")]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime ExpiredToken { get; set; }
        public int OTP { get; set; }
        public Boolean IsUsed { get; set; }

        //relation
        [JsonIgnore]
        public virtual Employee Employees { get; set; }
        public virtual ICollection<AccountRole> accountRoles { get; set; }
    }
}