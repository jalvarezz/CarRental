﻿using AttributeRouting.Web.Http;
using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using CarRental.Web.Core;
using CarRental.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRental.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    [Authorize]
    [UsesDisposableService]
    public class CustomerApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CustomerApiController(IAccountService accountService)
        {
            _AccountService = accountService;
        }

        IAccountService _AccountService;

        protected override void RegisterServices(List<global::Core.Common.Contracts.IServiceContract> disposableServices)
        {
            disposableServices.Add(_AccountService);
        }

        [HttpGet]
        [GET("api/customer/account")]
        public HttpResponseMessage GetCustomerAccountInfo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Account account = _AccountService.GetCustomerAccountInfo(User.Identity.Name);

                response = request.CreateResponse<Account>(System.Net.HttpStatusCode.OK, account);

                return response;
            });
        }

        [HttpPost]
        [POST("api/customer/account")]
        public HttpResponseMessage UpdateCustomerAccountInfo(HttpRequestMessage request, Account accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ValidateAuthorizedUser(accountModel.LoginEmail);

                List<string> errors = new List<string>();

                List<State> states = UIHelper.GetStates();
                State state = states.Where(item => item.Abbrev.ToUpper() == accountModel.State.ToUpper()).FirstOrDefault();

                if (state == null)
                    errors.Add("Invalid state.");

                // trim out the / in the exp date
                accountModel.ExpDate = accountModel.ExpDate.Substring(0, 2) + accountModel.ExpDate.Substring(3, 2);

                if (errors.Count == 0)
                {
                    _AccountService.UpdateCustomerAccountInfo(accountModel);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                {
                    response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, errors.ToArray());
                }

                return response;
            });
        }
    }
}