using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Common.ServiceModel
{
    public abstract class UserClientBase<T> : ClientBase<T> where T : class
    {
        public UserClientBase()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            MessageHeader<string> header = new MessageHeader<string>(username);

            OperationContextScope contextScope = new OperationContextScope(InnerChannel);

            OperationContext.Current.OutgoingMessageHeaders.Add(header.GetUntypedHeader("String", "System"));
        }
    }
}
