using System;
using System.Collections.Generic;
using System.Linq;
using Dandelion.Factory.Exceptions;
using Dandelion.Factory.Extensions;

namespace Dandelion.Factory
{
    public class SpecialistSchool<T> : ISeedPlanter<T>
    {
        internal SpecialistSchool() { }

        public IPlantGrower<T> From<T1>(T1 material)
        {
            return new PlantGrower<T1>(material);
        }

        public IManyPlantGrower<T> FromMany<T1>(IEnumerable<T1> seeds)
        {
            return new ManyPlantGrower<T1>(seeds);
        }
        class ManyPlantGrower<T1> : IManyPlantGrower<T>
        {
            private readonly IEnumerable<T1> _seeds;
            private readonly object _mutex = new object();
            private T[] _result;
            private int _numDone;
            private bool _beenUsed;
            public ManyPlantGrower(IEnumerable<T1> seeds)
            {
                _seeds = seeds.ToList();
            }

            public void Now(Action<IEnumerable<T>> onFullyGrown)
            {
                if (_beenUsed) throw new InvalidOperationException("The method 'Now' may only be called once per instance of ManyPlantGrower");
                _beenUsed = true;
                _result = new T[_seeds.Count()];
                var any =
                    _seeds.All((seed, i) =>
                        Container.Instance.ResolveChains<T1, T>()
                            .Any(chain => ExecuteFunc(i, seed, chain, onFullyGrown)));
                if (!any)
                    throw new ChainBrokenException();
            }
            private bool ExecuteFunc(int index, T1 seed, ChainLink chain, Action<IEnumerable<T>> onFullyGrown)
            {
                return
                    chain.NextAction(seed,
                          res =>
                          {
                              lock (_mutex)
                              {
                                  _result[index] = (T)res;
                                  if (++_numDone == _result.Length)
                                      onFullyGrown(_result);
                              }
                          });
            }
        }

        class PlantGrower<T1> : IPlantGrower<T>
        {
            private readonly T1 _material;
            public PlantGrower(T1 material)
            {
                _material = material;
            }

            public void Now(Action<T> onFullyGrown)
            {
                var any =
                    Container.Instance.ResolveChains<T1, T>().Any(
                        chain => chain.NextAction(_material, o => onFullyGrown((T)o)));
                if (!any)
                    throw new ChainBrokenException();
            }
        }
    }

    public interface ISeedPlanter<T>
    {
        IPlantGrower<T> From<T1>(T1 seed);
        IManyPlantGrower<T> FromMany<T1>(IEnumerable<T1> seeds);
    }

    public interface IPlantGrower<T>
    {
        void Now(Action<T> onFullyGrown);
    }

    public interface IManyPlantGrower<T>
    {
        void Now(Action<IEnumerable<T>> onFullyGrown);
    }
}