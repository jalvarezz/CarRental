using System;
using StructureMap.Pipeline;

namespace CarRental.Business.Managers.Pipeline
{
    public class LifecycleStrategy : ILifecycleStrategy
    {
        private Func<bool> _isValid;
        private ILifecycle _lifecycle;
        private Func<ILifecycle> _create;
        private Action<IObjectCache> _dispose;

        public LifecycleStrategy(Func<ILifecycle> create, Action<IObjectCache> dispose) : this(() => true, create, dispose) { }
        public LifecycleStrategy(Func<bool> isValid, Func<ILifecycle> create) : this(isValid, create, null) { }
        public LifecycleStrategy(Func<ILifecycle> create) : this(() => true, create, null) { }

        public LifecycleStrategy(Func<bool> isValid, Func<ILifecycle> create, Action<IObjectCache> dispose)
        {
            _isValid = isValid;
            _create = create;
            _dispose = dispose;
        }

        public bool IsValid()
        {
            return _isValid();
        }

        public ILifecycle Lifecycle
        {
            get
            {
                if (_lifecycle == null)
                    if (_dispose == null) _lifecycle = _create();
                    else _lifecycle = new DisposableLifecycleProxy(_create(), _dispose);
                return _lifecycle;
            }
        }
    }
}


