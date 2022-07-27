using System;
using System.Reflection;
using System.Linq;

namespace DefaultAppDomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the default AppDomain *****\n");
            DisplayDADStats();
            Console.WriteLine("\n**********************************************\n");
            ListAllAssembliesInAppDomain();
        }
        private static void DisplayDADStats()
        {
            // Получить доступ к домену приложения для текущего потока.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            // Вывести разнообразные статистические данные об этом домене.
            Console.WriteLine("Name of this domain: {0}", defaultAD.FriendlyName);
            Console.WriteLine("ID of domain in this process: {0}", defaultAD.Id);
            Console.WriteLine("Is this the default domain?: {0}",defaultAD.IsDefaultAppDomain());
            Console.WriteLine("Base directory of this domain: {0}",defaultAD.BaseDirectory);
        }
        static void ListAllAssembliesInAppDomain()
        {
            // Получить доступ к домену приложения для текущего потока.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            // Извлечь все сборки, загруженные в стандартный домен приложения.
            var loadedAssemblies = from a in defaultAD.GetAssemblies()
                                   orderby a.GetName().Name
                                   select a;
            // Assembly[] loadedAssemblies = defaultAd.GetAssemblies();
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",defaultAD.FriendlyName);
            foreach(var a in loadedAssemblies)
            {
                Console.WriteLine("-> Name: {0}", a.GetName().Name);
                Console.WriteLine("-> Version: {0}\n",a.GetName().Version);
            }
        }
    }
}
