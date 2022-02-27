using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
