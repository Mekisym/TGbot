using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TGbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Console.WriteLine("Working...");
            Console.ReadKey();
        }
    }
}
