using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using ExtensionMethods;

namespace Dynamic
{
    public class EventBriteConsumer : DynamicObject
    {
        private const string BaseUrl = "https://www.eventbrite.com/json/";
        private const string ApiKey = "YourKeyHere";
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var name = binder.Name.PascalToSnakeCase();

            var arguments = args
                .Select(o => o.ToString())
                .Zip(binder.CallInfo.ArgumentNames, (o, s) => new KeyValuePair<string, string>(s, o))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            result = Get(name, arguments);
            return true;
        }

        private static async Task<string> Get(string method, Dictionary<string, string> arguments)
        {
            Console.WriteLine("Calling " + BaseUrl + method);
            using (var client = new HttpClient())
            {
                var args = arguments.Select(kv => kv.Key + "=" + kv.Value).Join("&");
                var url = string.Format("{0}{1}?app_key={2}&{3}", BaseUrl, method, ApiKey, args);
                return await client.GetStringAsync(url);
            }
        }
    }
}