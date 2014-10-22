using StructureMap.Pipeline;

namespace CarRental.Business.Managers.Pipeline
{
    public interface ILifecycleStrategy
    {
        bool IsValid();
        ILifecycle Lifecycle { get;}
    }
}


