using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GeneralOffice: BaseEntity
    {
        public string Name { get; set; }
        public string Priority { get; set; }
        
    }
}
