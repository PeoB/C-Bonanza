using System;
using System.Collections.Generic;

namespace Dandelion.Factory
{
    public interface ICanGrowFrom<TIn, TOut>
    {
        bool Grow(TIn seed, Action<TOut> fullyGrown);
    }
    public interface ICanGrowManyFrom<TIn, TOut> : ICanGrowFrom<TIn, IEnumerable<TOut>> { }
}