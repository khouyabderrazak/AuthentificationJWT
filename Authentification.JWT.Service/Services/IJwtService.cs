using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification.JWT.Service.Services
{
    public interface IJwtService
    {
         Task<string> GenerateToken(int userId);
    }
}
