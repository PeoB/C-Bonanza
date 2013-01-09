using System.Collections.Generic;

namespace Dandelion.Factory
{
    public static class PlantSchool
    {
        public static ISeedPlanter<T> Grow<T>()
        {
            return new SpecialistSchool<T>();
        }


        public static ISeedPlanter<IEnumerable<T>> GrowMany<T>()
        {
            return new SpecialistSchool<IEnumerable<T>>();
        }
    }
}