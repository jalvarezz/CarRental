using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Contracts {
    public interface IBusinessEngine {
    }

    public interface IBusinessEngine<T> : IBusinessEngine 
        where T : class, new()
    {
    }
}
