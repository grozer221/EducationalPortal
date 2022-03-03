using EducationalPortal.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Auth.DTO
{
    public class AuthResponse
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}
