using Dandelion.Factory;
using Expressions.Growing;

namespace Expressions
{
    public class Bootstrap
    {
        public static void DoIt()
        {
            Container.Instance.Register(()=>new StringFetcher());
        } 
    }
}