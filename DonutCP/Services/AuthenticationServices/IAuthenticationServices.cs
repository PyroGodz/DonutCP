using DonutCP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonutCP.Services.AuthenticationServices
{
    class IAuthenticationServices
    {
        public enum RegistrationResult
        {
            Success,
            PasswordsDoNotMatch,
            EmailAlreadyExists,
            UsernameAlreadyExists
        }
        public interface IAuthenticationService
        {
            Task<bool> Register(string email, string username, string password);
            Task<Users> Login(string username, string password);
        }
    }
}
