using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNews.Models.Models
{
    [Table("T_NewGroups")]
    public class NewsGroup:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Id is not identity
        public int NewGroupId { get; set; }
        [Required]
        [MaxLength(200)]
        public string NewGroupTitle { get; set; }
        [MaxLength(100)]
        public string ImageName { get; set; }

        public virtual IEnumerable<News> Newses { get; set; }
    }
}
