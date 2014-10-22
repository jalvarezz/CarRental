using CarRental.Data.Contracts;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers.StructureMap
{
    public class StructureMapServiceInspector : IDispatchMessageInspector, IParameterInspector
    {
        private readonly Container _Container;

        public StructureMapServiceInspector(Container container)
        {
            _Container = container;
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //Console.WriteLine("StructureMapServiceInspector.AfterReceiveRequest called.");

            return null;//_Container.GetInstance<IUnitOfWork>();
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            //Console.WriteLine("StructureMapServiceInspector.BeforeSendReply called.");

            //var UnitOfWork = correlationState as IUnitOfWork;

            //if (UnitOfWork != null)
            //{
            //    UnitOfWork.Commit();
            //    UnitOfWork.Dispose();
            //}
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            //Console.WriteLine("StructureMapServiceInspector.AfterCall called.");
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            //Console.WriteLine("StructureMapServiceInspector.BeforeCall called.");
            return null;
        }
    }
}
