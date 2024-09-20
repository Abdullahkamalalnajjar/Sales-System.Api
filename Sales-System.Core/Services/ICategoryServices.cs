using Sales_System.Core.Dtos.Category;
using Sales_System.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Core.Services
{
    public interface ICategoryServices
    {
        public Task<Category> CreateCategoryAsync(CategoryDto category);
        public Task<Category> UpdateCategoryAsync(CategoryDto category);
        public Task<bool> DeleteCategoryAsync(int  categoryid);
        public Task<Category> GetCategoryByIdAsync(int categoryid);
    }
}
