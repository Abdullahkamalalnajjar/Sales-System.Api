using Sales_System.Core.Dtos.Invoice;
using Sales_System.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Core.Services
{
    public interface IInvoiceServices
    {
        public Task<Invoice> CreateInvoiceAsync(InvoiceDto invoiceDto);
        public Task<bool> UpdateInvoiceStatusAsync(int invoiceId, string invoiceStatus);
        public Task<string> DeleteInvoiceAsync(int  invoiceId);  
        public Task<InvoiceDto> GetInvoiceByidAsync(int invoiceId);
        public Task<List<TotalinvoiceDto>> GetTotalInvoiceAsync();

    }
}
