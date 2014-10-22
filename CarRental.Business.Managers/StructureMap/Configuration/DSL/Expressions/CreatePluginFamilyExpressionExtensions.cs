using StructureMap.Configuration.DSL.Expressions;
using CarRental.Business.Managers.Pipeline;

namespace CarRental.Business.Managers.Configuration.DSL.Expressions
{
    public static class CreatePluginFamilyExpressionExtensions
    {
        public static CreatePluginFamilyExpression<PLUGINTYPE> LifecycleStrategiesAre<PLUGINTYPE>(this CreatePluginFamilyExpression<PLUGINTYPE> expression, params ILifecycleStrategy[] strategies)
        {
            expression.LifecycleIs(new StrategicLifecycle(strategies));
            return expression;
        }
    }
}


