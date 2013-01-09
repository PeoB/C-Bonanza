using System;
using System.Net.Http;
using Dandelion.Factory;

namespace Expressions.Growing
{
    public class StringFetcher:ICanGrowFrom<Uri,string>
    {
        public bool Grow(Uri seed, Action<string> fullyGrown)
        {
            using (var client = new HttpClient())
            {
                var result=client.GetAsync(seed).Result;
                var res = result.Content.ReadAsStringAsync().Result;
                fullyGrown(res);
            }
            return true;
        }
    }
}