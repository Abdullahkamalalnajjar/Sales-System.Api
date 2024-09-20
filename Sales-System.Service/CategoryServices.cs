using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_System.Core.Dtos.Category;
using Sales_System.Core.Entities;
using Sales_System.Core.Repository;
using Sales_System.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Service
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServices(IGenericRepository<Category> categoryRepository , IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }
        public async Task<Category> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var result= await _categoryRepository.GetTableNoTracking().Where(c=>c.CategoryName==categoryDto.CategoryName).FirstOrDefaultAsync();

            if (result==null)
            {
                var categoryMapper = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.AddAsync(categoryMapper);
                return categoryMapper;
            }
            return null;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            // البحث عن الفئة في قاعدة البيانات باستخدام الـ ID
            var category = await _categoryRepository.GetTableNoTracking()
                .Where(c => c.Id==categoryId)
                .FirstOrDefaultAsync();

            if ( category!=null )
            {
                // حذف الفئة من قاعدة البيانات
                await _categoryRepository.DeleteAsync(category);
                return true; // العودة بالقيمة true للإشارة إلى نجاح الحذف
            }

            // إذا لم يتم العثور على الفئة، العودة بالقيمة false
            return false;
        }


        public async Task<Category> GetCategoryByIdAsync(int categoryid)
        {
           var result = await _categoryRepository.GetByIdAsync(categoryid);
            if (result!=null) { return result; }
            return null;
        }

        public async Task<Category> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var existingCategory = await _categoryRepository.GetTableNoTracking()
                .Where(c => c.CategoryName.Equals(categoryDto.CategoryName))
                .FirstOrDefaultAsync();

            if ( existingCategory!=null )
            {
                existingCategory.CategoryName=categoryDto.CategoryName; 

                _mapper.Map(categoryDto, existingCategory);

                await _categoryRepository.UpdateAsync(existingCategory);

                return existingCategory;
            }

            return null;
        }

    }
}
