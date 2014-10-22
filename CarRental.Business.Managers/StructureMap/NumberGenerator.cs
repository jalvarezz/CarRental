using System;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace CarRental.Business.Managers
{
    public interface INumberGenerator : IDisposable
    {
        int Number { get; }
    }

    public class NumberGenerator : INumberGenerator
    {
        private static int _current;

        public NumberGenerator()
        {
            Number = Interlocked.Increment(ref _current);
            Diagnostics.DebugWrite("Object created");
        }

        public int Number { get; private set; }

        public void Dispose()
        {
            Diagnostics.DebugWrite("Object disposed");
        }
    }
}
