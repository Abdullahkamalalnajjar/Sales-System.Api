using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Core.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? Status { get; set; }
        public string? Customer { get; set; }
        public string? Governorate { get; set; }
        public string? Note { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Page { get; set; }
        public string? Shipping { get; set; }
        public bool IsUrgent { get; set; } // فاتورة مستعجلة
        public bool NotReplied { get; set; } // لم يتم الرد
    }

}
