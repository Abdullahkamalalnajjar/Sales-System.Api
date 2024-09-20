using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales_System.Core.Entities;
namespace Sales_System.Repository.Data.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // إعداد Fluent API لصنف

            builder.HasKey(c => c.Id); // المفتاح الأساسي

            builder.Property(c => c.CategoryName)
                    .IsRequired() // مطلوب
                    .HasMaxLength(100); // الحد الأقصى لطول اسم الصنف

            builder.Property(c => c.Quantity)
                    .IsRequired(); // الكمية مطلوبة

            builder.Property(c => c.Price)
                    .HasColumnType("decimal(18,2)") // تحديد نوع الحقل ليكون decimal بدقة 18 و 2 خانات عشرية
                    .IsRequired(); // السعر مطلوب

            builder.Property(c => c.Discount)
                    .HasColumnType("decimal(18,2)") // نفس النوع لحقل الخصم
                    .HasDefaultValue(0); // قيمة الخصم الافتراضية 0

        }
    }
}
