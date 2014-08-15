using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Extensions
{
    public static class MefExtensions
    {
        public static object GetExportedValueByType(this CompositionContainer container, Type type)
        {
            // get a reference to the GetExportedValue<T> method
            MethodInfo methodInfo = container.GetType().GetMethods()
                                      .Where(d => d.Name == "GetExportedValue"
                                                  && d.GetParameters().Length == 0).First();
            
            // create an array of the generic types that the GetExportedValue<T> method expects
            Type[] genericTypeArray = new Type[] { type };

            // add the generic types to the method
            methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);

            // invoke GetExportedValue<type>()
            return methodInfo.Invoke(container, null);
        }

        public static IEnumerable<object> GetExportedValuesByType(this CompositionContainer container, Type type)
        {
            // get a reference to the GetExportedValue<T> method
            MethodInfo methodInfo = container.GetType().GetMethods()
                                      .Where(d => d.Name == "GetExportedValues"
                                                  && d.GetParameters().Length == 0).First();

            // create an array of the generic types that the GetExportedValue<T> method expects
            Type[] genericTypeArray = new Type[] { type };

            // add the generic types to the method
            methodInfo = methodInfo.MakeGenericMethod(genericTypeArray);

            // invoke GetExportedValues<type>()
            return (IEnumerable<object>)methodInfo.Invoke(container, null).;
        }
    }
}
