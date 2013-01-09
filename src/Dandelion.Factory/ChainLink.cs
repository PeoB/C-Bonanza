using System;
using System.Collections.Generic;
using System.Linq;

namespace Dandelion.Factory
{
    public class ChainLink
    {
        private readonly ChainLink[] _links;
        private readonly Func<object, Action<object>, bool> _middleAction;
        private int _index;
        private object _result;
        public ChainLink(Func<object, Action<object>, bool> middleAction)
        {
            _middleAction = middleAction;
        }

        public ChainLink(IEnumerable<ChainLink> links, Func<object, Action<object>, bool> middleAction)
        {
            _links = links.ToArray();
            _middleAction = middleAction;
        }

        public bool NextAction(object o, Action<object> action)
        {
            if (_links == null) return _middleAction(o, action);
            if (_index >= _links.Count())
                return false;
            if (_index > 0)
                CallNextAction(action);
            return _middleAction(o, res =>
                                        {
                                            _result = res;
                                            CallNextAction(action);
                                        });

        }

        private void CallNextAction(Action<object> action)
        {
            _links[_index].NextAction(_result, action);
            ++_index;
        }

        public static ChainLink Create(IEnumerable<ChainLink> resolveChain, Func<object, Action<object>, bool> func)
        {
            if (resolveChain == null) return null;
            var list = resolveChain.ToList();
            return !list.Any() ? null : new ChainLink(list, func);
        }

        public static ChainLink Create(Func<object, Action<object>, bool> resolveChain)
        {
            return resolveChain == null ? null : new ChainLink(resolveChain);
        }
    }
}