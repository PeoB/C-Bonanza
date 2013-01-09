using System;
using System.Threading.Tasks;

namespace Dynamic
{
    class Program
    {
        static void Main()
        {
            DoIt().Wait();
        }

        static async Task DoIt()
        {
            dynamic api = new EventBriteConsumer();
            //var result = api.EventGet(id: 4226892750);
            var result = api.OrganizerListEvents(id: 391611223);
           

            Console.WriteLine(await result);




            // var result2 = api.event_get(id: 4226892750);
            //Console.WriteLine(await result2);
        }
    }
}
