using Authentification.JWT.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.DAL.Dependency
{
    public static class AddDbContextService
    {
        public static void AddAppDBContextService(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDbContext<AppDbContext>(
                   options => options.UseSqlServer(_configuration.GetConnectionString("connectionString"))
             );
        }
    }
}
