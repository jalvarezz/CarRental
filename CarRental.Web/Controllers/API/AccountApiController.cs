using AttributeRouting.Web.Http;
using CarRental.Web.Core;
using CarRental.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace CarRental.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountApiController(ISecurityAdapter securityAdapter)
        {
            _SecurityAdapter = securityAdapter;
        }

        ISecurityAdapter _SecurityAdapter;

        [HttpPost]
        [POST("api/account/login")]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody]AccountLoginModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _SecurityAdapter.Initialize();

                bool success = _SecurityAdapter.Login(accountModel.LoginEmail, accountModel.Password, accountModel.RememberMe);

                if (success)
                {
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized login attempt.");
                }

                return response;
            });
        }

        [HttpPost]
        [POST("api/account/register/validate1")]
        public HttpResponseMessage ValidateRegistrationStep1(HttpRequestMessage request, [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<string> errors = new List<string>();

                List<State> states = UIHelper.GetStates();
                State state = states.Where(item => item.Abbrev.ToUpper() == accountModel.State.ToUpper()).FirstOrDefault();

                if (state == null)
                    errors.Add("Invalid state.");

                Match matchZipCode = Regex.Match(accountModel.ZipCode, @"^\d{5}(?:[-\s]\d{4})?$");
                if (!matchZipCode.Success)
                    errors.Add("Zip code is in an invalid format.");

                if (errors.Count == 0)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());

                return response;
            });
        }

        [HttpPost]
        [POST("api/account/register/validate2")]
        public HttpResponseMessage ValidateRegistrationStep2(HttpRequestMessage request, [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if(!_SecurityAdapter.UserExists(accountModel.LoginEmail))
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, new string[] { "An account is already registered with that email." });

                return response;
            });
        }

        [HttpPost]
        [POST("api/account/register/validate3")]
        public HttpResponseMessage ValidateRegistrationStep3(HttpRequestMessage request, [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                List<string> errors = new List<string>();

                if (accountModel.CreditCard.Length != 16)
                    errors.Add("Credit card number is in an invalid format.");

                Match matchZipCode = Regex.Match(accountModel.ExpDate, @"(0[1-9]|1[0-2])\/[0-9]{2}", RegexOptions.IgnoreCase);
                if (!matchZipCode.Success)
                    errors.Add("Expiration date is invalid.");

                if (errors.Count == 0)
                    response = request.CreateResponse(HttpStatusCode.OK);
                else
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());

                return response;
            });
        }

        [HttpPost]
        [POST("api/account/register")]
        public HttpResponseMessage CreateAccount(HttpRequestMessage request, [FromBody]AccountRegisterModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // revalidate all steps to ensure this operation is secure against hacks
                if(ValidateRegistrationStep1(request, accountModel).IsSuccessStatusCode &&
                   ValidateRegistrationStep2(request, accountModel).IsSuccessStatusCode &&
                   ValidateRegistrationStep3(request, accountModel).IsSuccessStatusCode)
                {
                    _SecurityAdapter.Initialize();

                    var result = _SecurityAdapter.Register(accountModel.LoginEmail, accountModel.Password,
                        propertyValues: new ApplicationUser
                        {
                            FirstName = accountModel.FirstName,
                            LastName = accountModel.LastName,
                            Email = accountModel.LoginEmail,
                            Address = accountModel.Address,
                            City = accountModel.City,
                            State = accountModel.State,
                            ZipCode = accountModel.ZipCode,
                            CreditCard = accountModel.CreditCard,
                            ExpDate = accountModel.ExpDate.Substring(0, 2) + accountModel.ExpDate.Substring(3, 2)
                        });

                    if (result.Succeeded)
                    {
                        _SecurityAdapter.Login(accountModel.LoginEmail, accountModel.Password, false);
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        request.CreateResponse<string[]>(HttpStatusCode.BadRequest, result.Errors.ToArray());
                    }
                }

                return response;
            });
        }
    }
}