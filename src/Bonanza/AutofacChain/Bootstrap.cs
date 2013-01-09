using System;
using System.Reflection;
using Autofac;
using AutofacChain.ChainParts;
using System.Linq;
using ExtensionMethods;

namespace AutofacChain
{
    public class Bootstrap
    {
        public static IContainer BuildIt()
        {
            var builder = new ContainerBuilder();
            builder.RegisterChain<IDescibe, DefaultDescription>();
            return builder.Build();
        }
    }

    public static class ContainerBuilderExtensions
    {
        public static void RegisterChain<T, TDefault>(this ContainerBuilder builder) where TDefault : T
        {
            var types = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(t => t.IsAssignableTo<T>() && t != typeof(TDefault) && t.IsClass && !t.IsAbstract)
                .Prepend(typeof(TDefault))
                .ToArray();

            types.ForEach((t, i) => Register<T>(builder, t, i !=0? types[i - 1] : null));
        }


        private static void Register<T>(ContainerBuilder builder, Type type, Type inType)
        {
            builder.RegisterType(type)
                .WithParameter((info, context) => info.ParameterType.IsAssignableTo<T>(), (info, context) => context.Resolve(inType)).AsSelf().As<T>();
        }
    }
}