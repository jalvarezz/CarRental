using CarRental.Web.Core;
using CarRental.Web.Helpers;
using CarRental.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Web.Services
{
    [Export(typeof(ISecurityAdapter))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityAdapter : ISecurityAdapter
    {
        private readonly ApplicationDbContext _context;
        private readonly Controller _Controller;
        private readonly UserManager<ApplicationUser> _UserManager;
        
        private IAuthenticationManager _AuthenticationManager;

        [ImportingConstructor]
        public SecurityAdapter()
        {
            _context = new ApplicationDbContext();

            _UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            _UserManager.UserValidator = new UserValidator<ApplicationUser>(_UserManager)
            {
                AllowOnlyAlphanumericUserNames = false,
            };
        }

        public SecurityAdapter(IAuthenticationManager authenticationManager)
        {
        }

        public SecurityAdapter(Controller controller)
        {
            _Controller = controller;
        }

        public void Initialize()
        {
            if (!_context.Database.Exists())
                _context.Database.Initialize(true);

            _AuthenticationManager = HttpContext.Current.Request.GetOwinContext().Authentication;
        }

        public IdentityResult Register(string loginEmail, string password, object propertyValues)
        {
            var userProperties = propertyValues as ApplicationUser;

            var user = new ApplicationUser() { 
                UserName = loginEmail,
                FirstName = userProperties.FirstName,
                LastName = userProperties.LastName,
                Email = userProperties.Email,
                Address = userProperties.Address,
                City = userProperties.City,
                State = userProperties.State,
                ZipCode = userProperties.ZipCode,
                CreditCard = userProperties.CreditCard,
                ExpDate = userProperties.ExpDate
            };

            return _UserManager.Create(user, password);
        }

        public bool Login(string loginEmail, string password, bool isPersistent)
        {
            var user = _UserManager.Find(loginEmail, password);
            if (user != null)
            {
                _AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = _UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                _AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

                return identity.IsAuthenticated;
            }

            return false;
        }

        public void Logout()
        {
            _AuthenticationManager.SignOut();
        }

        public bool ChangePassword(string loginEmail, string oldPassword, string newPassword)
        {
            var user = _UserManager.Find(loginEmail, oldPassword);

            if (user != null)
            {
                IdentityResult result = _UserManager.ChangePassword(user.Id, oldPassword, newPassword);

                return result.Succeeded;
            }

            return false;
        }

        public bool UserExists(string loginEmail)
        {
            return _UserManager.FindByName(loginEmail) != null;
        }
    }
}