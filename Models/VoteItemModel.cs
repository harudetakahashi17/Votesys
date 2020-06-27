using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Votesys.Models
{
    [Table("TB_M_VoteItems")]
    public class VoteItemModel
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public int SupportCount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public string Category { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
