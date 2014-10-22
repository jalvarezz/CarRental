using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarRental.Business.Managers
{
    public class Diagnostics
    {
        public static string WhatAmI()
        {
            if (OperationContext.Current != null) return "WCF";
            return "Console";
        }

        public static string DebugWrite<T>(T message)
        {
            string text = string.Format("{0} in {1} on thread {2} at {3}", message, WhatAmI(),
                            Thread.CurrentThread.ManagedThreadId, DateTime.Now);
            Debug.WriteLine(text);
            return text;
        }
    }
}
