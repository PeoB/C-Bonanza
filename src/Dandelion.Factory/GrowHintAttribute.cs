using System;
using System.Linq;

namespace Dandelion.Factory
{
    public class GrowHintAttribute : Attribute
    {
        public Type[] GrowOrder { get; set; }

        public Type OutputType { get; set; }

        public GrowHintAttribute(params Type[] growOrder)
        {
            OutputType = growOrder.Last();
            GrowOrder = growOrder.Take(growOrder.Count() - 1).ToArray();
        }
    }
}