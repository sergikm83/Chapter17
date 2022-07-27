using System;
using System.Linq;

namespace CustomAppDomains
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void MakeNewDomain()
        {
            // Создать новый домен приложения в текущем процессе
            // и вывести список загруженных сборок.
            AppDomain newAD = AppDomain.CreateDomain(Guid.NewGuid().ToString());
            ListAllAssembliesInAppDomain(newAD);
        }

        static void ListAllAssembliesInAppDomain(AppDomain ad)
        {
            // Получить все сборки, загруженные в стандартный домен приложения.
            var loadedAssemblies = from a in ad.GetAssemblies()
                                   orderby a.GetName().Name
                                   select a;
            // Assembly[] loadedAssemblies = defaultAd.GetAssemblies();
            Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n", ad.FriendlyName);
            foreach (var a in loadedAssemblies)
            {
                Console.WriteLine("-> Name: {0}", a.GetName().Name);
                Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
            }
        }
    }
}
