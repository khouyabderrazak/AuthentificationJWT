using Authentification.JWT.DAL.Data;
using Microsoft.EntityFrameworkCore;
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
        public static void AddAppDBContextService(this IServiceCollection services)
        {

            var connectionString = "Server=RAB68WZ2H2;Database=authDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AppDbContext>(
                   options => options.UseSqlServer(connectionString)
             );
        }
    }
}
