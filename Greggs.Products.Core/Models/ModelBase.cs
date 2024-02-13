using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greggs.Products.Core.Models
{
    public class ModelBase
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedDate { get; set; } = DateTime.UtcNow;
        public string? DeletedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }

    }
}
