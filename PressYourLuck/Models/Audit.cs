using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS3_RM5831.Models
{
    public class Audit
    {
        public int AuditId { get; set; }
        public string PlayerName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public double? Amount { get; set; }

        public string AuditTypeId { get; set; }
        public AuditType AuditType { get; set; }
    }

}
