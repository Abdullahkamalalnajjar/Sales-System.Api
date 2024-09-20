using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Sales_System.Api.Helpers;
using Sales_System.Core.Entities.Identity;
using Sales_System.Core.Repository;
using Sales_System.Core.Services;
using Sales_System.Repository.Identity;
using Sales_System.Respository;
using Sales_System.Service;

namespace Sales_System.Api.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
 

            Services.AddScoped(typeof(IInvoiceServices), typeof(InvoiceServices));
            Services.AddScoped(typeof(ICategoryServices), typeof(CategoryServices));

            #region Config AutoMapper 
            Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(ProfilesMapping)));
            #endregion

            #region cfg igeneric
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion       

            return Services;

        }
    }



}
