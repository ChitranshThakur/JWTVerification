using JWTVerification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTVerification.Repository
{
    public interface IJWtManagerRepository
    {
        Tokens Authenticate(Users users); 
    }
}
 