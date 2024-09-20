using AutoMapper;
using Sales_System.Core.Dtos.Category;
using Sales_System.Core.Dtos.Invoice;
using Sales_System.Core.Entities;

namespace Sales_System.Api.Helpers
{
    public class ProfilesMapping:Profile
    {
        public ProfilesMapping() {

            #region Map InvoiceDto to Invoice
            CreateMap<InvoiceDto,Invoice>().ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion

            #region Map Invoice to InvoiceDto
            CreateMap<Invoice, InvoiceDto>();
            #endregion

            #region Map InvoiceDto to Invoice
            CreateMap<CategoryDto, Category>().ForMember(dest => dest.Id, opt => opt.Ignore());
            #endregion
        }
    }
}
