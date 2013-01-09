using System;
using System.Collections.Generic;
using System.Linq;
using Dandelion.Factory.Exceptions;
using Dandelion.Factory.Extensions;

namespace Dandelion.Factory
{
    public class Container
    {
        private static readonly Container _instance = new Container { MaxDepth = 3 };

        public static Container Instance { get { return _instance; } }
        private readonly MatrixDictionary<Type, Type, Func<object, Action<object>, bool>> _dictionary = new MatrixDictionary<Type, Type, Func<object, Action<object>, bool>>();
        private List<Type> _addedHintTypes = new List<Type>();
        public void Register<T, T2>(ICanGrowFrom<T, T2> canGrowFrom)
        {
            Register(() => canGrowFrom);
        }
        public void Register<T, T2>(Func<ICanGrowFrom<T, T2>> func)
        {
            _dictionary[typeof(T), typeof(T2)].Add((o, a) => func().Grow((T)o, res => a(res)));
        }
        internal IEnumerable<Func<T, Action<T2>, bool>> Resolve<T, T2>()
        {
            return _dictionary[typeof(T), typeof(T2)].Select<Func<object, Action<object>, bool>, Func<T, Action<T2>, bool>>(a => (o, a2) => a(o, res => a2((T2)res)));
        }
        internal IEnumerable<Func<object, Action<object>, bool>> Resolve(Type inType, Type outType)
        {
            var results = ResolveFromBaseTypes(inType, outType).ToList();
            return results.Any() ? results : ResolveFromInterfaces(inType, outType);
        }

        private IEnumerable<Func<object, Action<object>, bool>> ResolveFromInterfaces(Type inType, Type outType)
        {
            var interfaces = inType.GetInterfaces();
            return interfaces.Select(i => _dictionary[i, outType]).FirstOrDefault(coll => coll.Any()) ?? Enumerable.Empty<Func<object, Action<object>, bool>>();
        }

        private IEnumerable<Func<object, Action<object>, bool>> ResolveFromBaseTypes(Type inType, Type outType)
        {
            if (inType == typeof(object)) return Enumerable.Empty<Func<object, Action<object>, bool>>();

            return _dictionary[inType, outType].Any()
                       ? _dictionary[inType, outType]
                       : ResolveFromBaseTypes(inType.BaseType, outType);
        }

        public int MaxDepth { get; set; }
        internal IEnumerable<ChainLink> ResolveChains<T, T1>()
        {
            return InheritedTypes(typeof(T)).Select(t => ResolveChain(t, typeof(T1))).FirstOrDefault(c => c.Any());
        }
        internal IEnumerable<ChainLink> ResolveChains(Type inType, Type outType)
        {
            return InheritedTypes(inType).Select(t => ResolveChain(t, outType).ToList()).FirstOrDefault(c => c.Any());
        }
        private IEnumerable<ChainLink> ResolveChain(Type inputType, Type outputType, int depth = 0)
        {
            if (depth >= MaxDepth) yield break;
            var i = _dictionary[inputType];
            var queue = new Queue<ChainBuilder>();
            i.ForEach(func => queue.Enqueue(new ChainBuilder(func.Key, func.Value)));

            while (queue.Count > 0)
            {
                var builder = queue.Dequeue();
                if (builder.OutputType == outputType)
                    foreach (var chain in builder.CreateChains())
                        yield return chain;
                else if (builder.Depth < MaxDepth)
                    _dictionary[builder.OutputType].ForEach(func => queue.Enqueue(builder.AddLink(func.Key, func.Value)));
            }
        }

        private void MakeSureHintsIsRegistered(Type inputType)
        {
            if (_addedHintTypes.Contains(inputType)) return;
            _addedHintTypes.Add(inputType);
            inputType.GetCustomAttributes(typeof(GrowHintAttribute), false).Cast<GrowHintAttribute>().ForEach(hint => RegisterSuggestion(inputType, hint.OutputType, hint.GrowOrder));
        }

        private IEnumerable<Type> InheritedTypes(Type type)
        {
            yield return type;
            var baseType = type.BaseType ?? typeof(object);
            while (baseType != typeof(object))
            {
                yield return baseType;
                baseType = baseType.BaseType ?? typeof(object);
            }
            foreach (var item in type.GetInterfaces())
            {
                yield return item;
            }
        }

        public void RegisterSuggestion<T, T1>(params Type[] chainParts)
        {
            RegisterSuggestion(typeof(T), typeof(T1), chainParts);
        }

        public void RegisterToManySuggestion<T, T1>(params Type[] chainParts)
        {
            RegisterSuggestion(typeof(T), typeof(IEnumerable<T1>), chainParts);
        }

        private void RegisterSuggestion(Type inType, Type outType, Type[] chainParts)
        {
            if (!chainParts.Any()) throw new ArgumentException("Atleast one chainpart must be set");
            if (!(chainParts.Any() && InheritedTypes(inType).Contains(chainParts[0])))
                chainParts = chainParts.Prepend(inType);
            chainParts = chainParts.Append(outType);
            _dictionary[inType, outType].Add(new GrowChain(chainParts).Grow);
        }
        class GrowChain : ICanGrowFrom<object, object>
        {
            private readonly Type[] _typeChain;


            public GrowChain(IEnumerable<Type> typeChain)
            {
                _typeChain = typeChain.ToArray();

            }

            public bool Grow(object seed, Action<object> fullyGrown)
            {
                Grow(seed, fullyGrown, 0);
                return true;
            }

            private void Grow(object seed, Action<object> fullyGrown, int chainIndex)
            {
                if (chainIndex >= _typeChain.Length - 1)
                {
                    fullyGrown(seed);
                    return;
                }
                var any = Instance.ResolveChains(_typeChain[chainIndex], _typeChain[chainIndex + 1]).Any(chain => chain.NextAction(seed, o => Grow(o, fullyGrown, chainIndex + 1)));
                if (!any)
                    throw new ChainBrokenException();
            }
        }
    }
}