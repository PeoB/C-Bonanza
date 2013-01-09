using System;
#if XBOX
#else
using System.Linq.Expressions;
using System.Linq;

#endif


namespace Dandelion.Factory.Extensions
{
    public static class GenericExtensions
    {
#if XBOX
#else
        
#endif
        public static void To<TIn, TOut>(this TIn material, Action<TOut> action)
        {
            PlantSchool.Grow<TOut>().From(material).Now(action);
        }
    }
}