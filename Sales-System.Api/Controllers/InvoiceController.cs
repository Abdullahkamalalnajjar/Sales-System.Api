using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales_System.Core.Dtos.Category;
using Sales_System.Core.Dtos.Invoice;
using Sales_System.Core.Entities;
using Sales_System.Core.Services;
using Sales_System.Helpers;
using System.Security.Claims;

namespace Sales_System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceServices;
        private readonly ICategoryServices _categoryServices;

        public InvoiceController(IInvoiceServices invoiceServices , ICategoryServices categoryServices)
        {
           _invoiceServices=invoiceServices;
           _categoryServices=categoryServices;
        }
        [Authorize]
        [HttpPost("create-invoice")]
        public async Task<ActionResult<ApiResponse<Invoice>>> CreateInvoiceAsync([FromBody]InvoiceDto invoiceDto)
        {
         
            
            var result = await _invoiceServices.CreateInvoiceAsync(invoiceDto);   
            if (result == null)
            {
                return BadRequest(new ApiResponse<string>(400,"Bad request"));
            }
            return Ok(new ApiResponse<Invoice>(200,"تم إضافة الفاتورة بنجاح",result));  

        }
        [HttpGet("get-invoiceByid")]
        public async Task<ActionResult<ApiResponse<Invoice>>> GetInvoiceByidAsync(int id)
        {
            var result = await _invoiceServices.GetInvoiceByidAsync(id);
            if ( result==null )
            {
                return NotFound(new ApiResponse<string>(404, "الفاتورة غير موجودة"));
            }
            return Ok(new ApiResponse<InvoiceDto>(200, "تم العثور علي الفاتورة بنجاح", result));
        }

        [Authorize]
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDto categoryDto)
        {


            var result = await _categoryServices.CreateCategoryAsync(categoryDto);
            if ( result==null )
            {
                return BadRequest(new ApiResponse<string>(400, "موجود من قبل"));
            }
            return Ok(new ApiResponse<Category>(200, "تم إضافة الصنف بنجاح", result));

        }

        [HttpGet("get-CategoryByid")]
        public async Task<ActionResult<ApiResponse<Category>>> GetCategoryByidAsync(int id)
        {
            var result = await _categoryServices.GetCategoryByIdAsync(id);
            if ( result==null )
            {
                return NotFound(new ApiResponse<string>(404, "الصنف غير موجودة"));
            }
            return Ok(new ApiResponse<Category>(200, "تم العثور علي الصنف بنجاح", result));
        }

        [HttpPut("update-CategoryByid")]
        public async Task<ActionResult<ApiResponse<Category>>> UpdateCategoeryByidAsync([FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryServices.UpdateCategoryAsync(categoryDto);
            if ( result==null )
            {
                return NotFound(new ApiResponse<string>(404, "الصنف غير موجودة"));
            }
            return Ok(new ApiResponse<Category>(200, "تم تعديل علي الصنف بنجاح", result));
        }
        [HttpDelete("delete-CategoryByid")]
        public async Task<IActionResult> DeleteCategoeryByidAsync( int categoryid)
        {
            var result = await _categoryServices.DeleteCategoryAsync(categoryid);
            if ( result==false )
            {
                return NotFound(new ApiResponse<string>(404, "الصنف غير موجودة"));
            }
            return Ok(new ApiResponse<bool>(200, "تم حذف  الصنف بنجاح", result));
        }

        [HttpGet("get-total-invoice-with-governorate")]
        public async Task<ActionResult<ApiResponse<List<TotalinvoiceDto>>>> GetTotalInvoiceWithGovernorate()
        {
            var result = await _invoiceServices.GetTotalInvoiceAsync();
            if ( result==null )
            {
                return NotFound(new ApiResponse<string>(404, "لا يوجد فواتير"));
            }
            return Ok(new ApiResponse<List<TotalinvoiceDto>>(200, "تم العثور علي فواتير", result));
        }
        [HttpPut("update-invoiceStatus")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateInvoiceStatus(int invoiceId, string invoiceStatus) { 
        
            var result= await _invoiceServices.UpdateInvoiceStatusAsync(invoiceId, invoiceStatus);  
            if ( result==false )
            {
                return BadRequest(new ApiResponse<bool>(400, "حدث خطأ اثناء التعديل علي حالة الفاتورة", result));

            }
            return Ok (new ApiResponse<bool>(200, "تم التعديل علي حالة الفاتورة", result));

        }
    }
}
