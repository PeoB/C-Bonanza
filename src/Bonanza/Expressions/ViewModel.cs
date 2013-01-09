using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionMethods;
using Expressions.Extensions;

namespace Expressions
{
    public class ViewModel
    {
        public ViewModel()
        {
            new Uri("http://google.se")
                .To(() => Content.Tap(ContentSet));
        }

        private static void ContentSet(string s)
        {
            Console.WriteLine("Content is now set... magic, eh?");
        }

        public string Content { get; private set; }

        public Task MayMoveOn
        {
            get
            {
                return Task.Run(() =>
                    {
                        while (Content == null)
                        {
                            Thread.Sleep(1000);
                        }

                    });
            }
        }
    }
}