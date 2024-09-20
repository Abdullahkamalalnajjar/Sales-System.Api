using Microsoft.AspNetCore.Identity;
using Sales_System.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_System.Core.Services
{
    public interface ITokenService
    {
        public Task<string> CreateTokenAsync(AppUser appUser , UserManager<AppUser> userManager);
    }
}
