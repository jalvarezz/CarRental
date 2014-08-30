using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Web.Core
{
    public interface ISecurityAdapter
    {
        void Initialize();
        IdentityResult Register(string loginEmail, string password, object propertyValues);
        bool Login(string loginEmail, string password, bool rememberMe);
        void Logout();
        bool ChangePassword(string loginEmail, string oldPassword, string newPassword);
        bool UserExists(string loginEmail);
    }
}
