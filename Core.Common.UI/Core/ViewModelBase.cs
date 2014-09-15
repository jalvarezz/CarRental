using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.UI.Core
{
    public abstract class ViewModelBase : ObjectBase
    {
        public abstract string ViewTitle { get; }
    }
}
