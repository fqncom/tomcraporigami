using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAppDemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("this is main function ThreadId = " + Thread.CurrentThread.ManagedThreadId);

            //ChangeText();

            //Console.WriteLine("this is main function ThreadId = " + Thread.CurrentThread.ManagedThreadId);

            string[] str = new[] { "1", "1", "1", "1", "1", "1" };
          var list =  GetList(str);
          foreach (var item in list)
          {
              Console.WriteLine(item);
          }


        }

        private static IEnumerable<int> GetList(params string[] str)
        {
            foreach (var item in str)
            {
                Console.WriteLine(item);
                yield return int.Parse(item);
            }
        }

        private static void ChangeText()
        {

            string[] str = {"1","2"};
            var a = (from i in str
                     select int.Parse(i)).ToList();


            Console.WriteLine("this is main function ThreadId = " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("this is main function ThreadId = " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
