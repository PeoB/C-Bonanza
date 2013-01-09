using System.Reflection;
using Autofac;

namespace Lazy
{
    public class Bootstrap
    {
        public static IContainer DoIt()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf();

            return builder.Build();
        }
    }
}