using Authentification.JWT.DAL.Data;
using Authentification.JWT.DAL.Dependency;
using Authentification.JWT.Service.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Dependency
{
    public static class AddAuthentificationServices
    {
        public static void AddAthentificationServices(this IServiceCollection services,IConfiguration _conf)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            //dbContext
            services.AddAppDBContextService(_conf);

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
