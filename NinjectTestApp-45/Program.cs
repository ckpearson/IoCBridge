using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectTestApp_45
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrap = new IoCBridge.Ninject.NinjectBootstrapper();
            bootstrap.Start();

            var tst = bootstrap.Get<ITest>();
            Console.WriteLine(tst.SayHello("Clint"));
            Console.ReadLine();
        }
    }

    public interface ITest
    {
        string SayHello(string name);
    }

    public class EnglishTest
        : ITest
    {
        public string SayHello(string name)
        {
            return "Hello, " + name;
        }
    }

    public class FrenchTest
        : ITest
    {
        public string SayHello(string name)
        {
            return "Bonjour, " + name;
        }
    }
}
