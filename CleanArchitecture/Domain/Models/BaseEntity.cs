using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        //[NotMapped]
        [/*Required,*/ DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //DefaultValue("getutcdate()")]
        [DefaultValue("getdate()")]
        [Column(TypeName = "datetime")]
        public DateTime? AddedDate { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
