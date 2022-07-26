using System;

namespace DefaultAppDomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        private static void DisplayDADStats()
        {
            // Получить доступ к домену приложения для текущего потока.
            AppDomain defaultAD = AppDomain.CurrentDomain;
            // Вывести разнообразные статистические данные об этом домене.
            Console.WriteLine("Name of this domain: {0}", defaultAD.FriendlyName);
            Console.WriteLine("ID of domain in this process: {)}", defaultAD.Id);
            Console.WriteLine("Is this the default domain?: {0}",defaultAD.IsDefaultAppDomain());
            Console.WriteLine("Base directory of this domain: {0}",defaultAD.BaseDirectory);
        }
    }
}
