using System;
using System.Diagnostics;
using System.Linq;

namespace ProcessManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Processes *****\n");
            ListAllRunningProcesses();
            Console.Write("-> Enter the process Id: ");
            int processId = int.Parse(Console.ReadLine());
            GetSpecificProcess(processId);
        }
        static void ListAllRunningProcesses()
        {
            // Получить все процессы на локальной машине, упорядоченные по PID.
            var runningProcs = from proc in Process.GetProcesses(".")
                               orderby proc.Id
                               select proc;
            // Вывести для каждого процесса идентификатор PID и имя.
            foreach(var p in runningProcs)
            {
                string info = $"-> PID: {p.Id}\tName: {p.ProcessName}";
                Console.WriteLine(info);
            }
            Console.WriteLine("\n***************************************\n");
            Console.WriteLine($"-> Total running processes: [{runningProcs.Count()}]");
            Console.WriteLine("\n***************************************\n");
        }
        // Если процесса с указанным PID не существует,
        // то сгенерируется исключение во время выполнения.
        static void GetSpecificProcess(int pID)
        {
            Process theProc = null;
            try
            {
                theProc = Process.GetProcessById(pID);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            string procInfo = $"-> Name: {theProc.ProcessName}\nPriority: {theProc.BasePriority}\nStart time: {theProc.StartTime}";
            Console.WriteLine(procInfo);
        }
        static void EnumThreadsForPid(int pID)
        {
            Process theProc = null;
            try
            {
                theProc = Process.GetProcessById(pID);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            // Вывести статистические сведения по каждому потоку в указанном процессе.
            Console.WriteLine("Here are the threads used by: {0}", theProc.ProcessName);
            ProcessThreadCollection theThreads = theProc.Threads;
            foreach(ProcessThread pt in theThreads)
            {
                string info = $"-> Thread ID: {pt.Id}\t" +
                    $"Start Time: {pt.StartTime.ToShortTimeString()}\t" +
                    $"Priority: {pt.PriorityLevel}";
                Console.WriteLine(info);
            }
            Console.WriteLine("\n***************************************\n");
        }
        static Process GetProcess(int pID)
        {
            Process theProc = null;
            try
            {
                theProc = Process.GetProcessById(pID);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return theProc;
        }
    }
}
