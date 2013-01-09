using System;
using System.Collections.Generic;
using System.Linq;
using Dandelion.Factory.Extensions;

namespace Dandelion.Factory
{
    internal class ChainBuilder
    {
        private readonly List<ICollection<Func<object, Action<object>, bool>>> _chainParts = new List<ICollection<Func<object, Action<object>, bool>>>();
        public Type OutputType { get; set; }

        public int Depth { get { return _chainParts.Count; } }

        public ChainBuilder(Type outputType, ICollection<Func<object, Action<object>, bool>> funcs)
        {
            _chainParts.Add(funcs);
            OutputType = outputType;
        }
        private ChainBuilder(Type outputType, IEnumerable<ICollection<Func<object, Action<object>, bool>>> enumerable)
        {
            _chainParts = enumerable.ToList();
            OutputType = outputType;
        }
        public IEnumerable<ChainLink> CreateChains()
        {
            var chainParts = Enumerable.Reverse(_chainParts).ToList();
            var first = chainParts.First().Select(ChainLink.Create);
            return chainParts.Skip(1).Aggregate(first,
                                                 (prev, current) => current.Select(func => ChainLink.Create(prev, func)));
        }

        public ChainBuilder AddLink(Type outputType, ICollection<Func<object, Action<object>, bool>> link)
        {
            return new ChainBuilder(outputType, _chainParts.ToArray().Append(link));
        }
    }
}