using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Votesys.Models
{
    [Table("TB_T_Vote")]
    public class VoteModel
    {
        [Key]
        public int VoteID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string ItemID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
