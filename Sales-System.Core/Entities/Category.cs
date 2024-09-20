using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }  // اسم الصنف
        public int Quantity { get; set; } // الكمية
        public decimal Price { get; set; } // السعر
        public decimal Discount { get; set; } // الخصم
    }

}
