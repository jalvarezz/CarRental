using System;
using System.Collections.Generic;
using StructureMap.Pipeline;
using StructureMap;

namespace CarRental.Business.Managers.Pipeline
{
    public class DisposableLifecycleProxy : ILifecycle
    {
        private ILifecycle _lifecycle;
        private Action<IObjectCache> _dispose;
        private Dictionary<IObjectCache, IObjectCache> _objectCaches = 
            new Dictionary<IObjectCache, IObjectCache>();

        public DisposableLifecycleProxy(ILifecycle lifecycle, Action<IObjectCache> dispose)
        {
            _lifecycle = lifecycle;
            _dispose = dispose;
        }

        public void CacheDisposed(IObjectCache objectCache)
        {
            if (!_objectCaches.ContainsKey(objectCache)) return;
            lock (_objectCaches) { _objectCaches.Remove(objectCache); }
        }

        public void EjectAll(ILifecycleContext context) { _lifecycle.EjectAll(context); }

        public IObjectCache FindCache(ILifecycleContext context)
        {
            IObjectCache objectCache = _lifecycle.FindCache(context);

            // This is here to ensure the close lambda is only executed once
            // per object cache. Not sure of a better way to handle this.
            if (!_objectCaches.ContainsKey(objectCache))
            {
                ObjectCacheProxy proxy = new ObjectCacheProxy(this, objectCache);
                lock (_objectCaches) { _objectCaches.Add(objectCache, proxy); }
                _dispose(proxy);
            }

            return _objectCaches[objectCache];
        }

        public string Description { get { return _lifecycle.Description; } }

        // ────────────────────────── Nested Types ──────────────────────────

        public class ObjectCacheProxy : IObjectCache, IDisposable
        {
            private IObjectCache _objectCache;
            private DisposableLifecycleProxy _lifecycleProxy;
            private bool _disposed;

            public ObjectCacheProxy(DisposableLifecycleProxy lifecycleProxy, IObjectCache objectCache)
            {
                _objectCache = objectCache;
                _lifecycleProxy = lifecycleProxy;
            }

            public void DisposeAndClear() 
            {
                _objectCache.DisposeAndClear(); 
                Dispose();
            }

            public void Dispose()
            {
                if (_disposed) return;

                _disposed = true;
                _lifecycleProxy.CacheDisposed(_objectCache);
            }

            public int Count { get { return _objectCache.Count; } }
            public void Eject(Type pluginType, Instance instance) { _objectCache.Eject(pluginType, instance); }
            public bool Has(Type pluginType, Instance instance) { return _objectCache.Has(pluginType, instance); }
            public object Get(Type pluginType, Instance instance, IBuildSession session) { return _objectCache.Get(pluginType, instance, session); }
        }
    }
}


