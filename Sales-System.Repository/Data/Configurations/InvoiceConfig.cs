using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales_System.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Repository.Data.Configurations
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id); // تعيين المفتاح الأساسي

            builder.Property(i => i.InvoiceDate)
                  .IsRequired(); // تعيين الحقل كـ required

            builder.Property(i => i.Status)
                  .IsRequired()
                  .HasMaxLength(50); // الحد الأقصى لطول الحقل 50

            builder.Property(i => i.Customer)
                  .IsRequired()
                  .HasMaxLength(100); // الحد الأقصى لطول اسم العميل 100

            builder.Property(i => i.Governorate)
                  .HasMaxLength(100); // الحد الأقصى لطول المحافظة 100

            builder.Property(i => i.Note)
                  .HasMaxLength(500); // الملاحظة بحد أقصى 500 حرف

            builder.Property(i => i.Phone)
                  .HasMaxLength(15); // رقم الهاتف بحد أقصى 15 حرف

            builder.Property(i => i.Address)
                  .HasMaxLength(200); // العنوان بحد أقصى 200 حرف

            builder.Property(i => i.Page)
                  .HasMaxLength(50); // الحد الأقصى للصفحة 50 حرف

            builder.Property(i => i.Shipping)
                  .HasMaxLength(100); // الحد الأقصى لحالة الشحن 100 حرف

            builder.Property(i => i.IsUrgent)
                  .HasDefaultValue(false); // القيمة الافتراضية لفاتورة مستعجلة هي false

            builder.Property(i => i.NotReplied)
                  .HasDefaultValue(false); // القيمة الافتراضية لم يتم الرد هي false
        }
    }
}
