using CarRental.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Web.Core
{
    public class UIHelper
    {
        public static List<State> GetStates()
        {
            return new List<State>()
            {
                new State { Abbrev = "FL", Name = "Florida" },
                new State { Abbrev = "NY", Name = "New York" }
            };
        }
    }
}