using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_System.Core.Dtos.Invoice;
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
    public class InvoiceServices : IInvoiceServices
    {
        private readonly IGenericRepository<Invoice> _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceServices(IGenericRepository<Invoice> invoiceRepository , IMapper mapper )
        {
            _invoiceRepository=invoiceRepository;
            _mapper=mapper;
        }
        public async Task<Invoice> CreateInvoiceAsync(InvoiceDto invoiceDto)
        {
            var invoiceMapper = _mapper.Map<Invoice>(invoiceDto); 

            await _invoiceRepository.AddAsync(invoiceMapper);

            return invoiceMapper;
        }

        public Task<string> DeleteInvoiceAsync(int invoiceId)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDto> GetInvoiceByidAsync(int invoiceId)
        {
           var check = await _invoiceRepository.GetByIdAsync(invoiceId);
            if (check != null)
            {
                var invoicemapper= _mapper.Map<InvoiceDto>(check);
                return invoicemapper;
            }
            return null;
        }

        public async Task<List<TotalinvoiceDto>> GetTotalInvoiceAsync()
        {
            var invoices = await _invoiceRepository.GetTableNoTracking().AsNoTracking() 
            .GroupBy(i => i.Governorate) // اجمع حسب المحافظة
            .Select(g => new TotalinvoiceDto
            {
                Governorate=g.Key,
                Total=g.Count() 
            })
            .ToListAsync(); 

            return invoices;

        }

        public async Task<bool> UpdateInvoiceStatusAsync(int invoiceId, string invoiceStatus)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            if ( invoice!= null) { 
            invoice.Status=invoiceStatus;
                await _invoiceRepository.UpdateAsync(invoice);  
                return true;
            }
            return false;
        }
    }
}
