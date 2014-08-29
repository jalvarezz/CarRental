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
        private readonly IAuthenticationManager _AuthenticationManager;
        private readonly Controller _Controller;

        public SecurityAdapter()
        {
            _context = new ApplicationDbContext();
        }

        public SecurityAdapter(IAuthenticationManager authenticationManager)
        {
            _AuthenticationManager = authenticationManager;
        }

        [ImportingConstructor]
        public SecurityAdapter(Controller controller)
        {
            _Controller = controller;
            _AuthenticationManager = controller.HttpContext.GetOwinContext().Authentication;
        }

        public SecurityAdapter(IAuthenticationManager authenticationManager, Controller controller)
        {
            _Controller = controller;
            _AuthenticationManager = authenticationManager;
        }

        public void Initialize()
        {
            if (!_context.Database.Exists())
                _context.Database.Initialize(true);
        }

        public IdentityResult Register(string loginEmail, string password, object propertyValues)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var user = new ApplicationUser() { UserName = loginEmail };
            return um.Create(user, password);
        }

        public bool Login(string loginEmail, string password, bool isPersistent)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var user = um.Find(loginEmail, password);
            if (user != null)
            {
                _AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                var identity = um.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                _AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

                return identity.IsAuthenticated;
            }

            return false;
        }

        public bool ChangePassword(string loginEmail, string oldPassword, string newPassword)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var user = um.Find(loginEmail, oldPassword);

            if (user != null)
            {
                IdentityResult result = um.ChangePassword(user.Id, oldPassword, newPassword);

                return result.Succeeded;
            }

            return false;
        }

        public bool UserExists(string loginEmail)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            return um.FindByName(loginEmail) != null;
        }
    }
}