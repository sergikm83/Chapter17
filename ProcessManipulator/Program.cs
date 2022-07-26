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
            //ListAllRunningProcesses();
            //Console.Write("-> Enter the process Id: ");
            //int processId = int.Parse(Console.ReadLine());
            //GetSpecificProcess(processId);
            //Console.WriteLine();
            //EnumThreadsForPid(processId);
            //Console.WriteLine();
            //EnumModsForPid(processId);
            StartAndKillProcess();
        }
        static void ListAllRunningProcesses()
        {
            // Получить все процессы на локальной машине, упорядоченные по PID.
            var runningProcs = from proc in Process.GetProcesses(".")
                               orderby proc.Id
                               select proc;
            // Вывести для каждого процесса идентификатор PID и имя.
            foreach (var p in runningProcs)
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
            Process theProc = GetProcess(pID);
            if (theProc != null)
            {
                string info = $"-> Name: {theProc.ProcessName}\nPriority: {theProc.BasePriority}\nStart time: {theProc.StartTime}";
                Console.WriteLine(info);
            }
            else
                Console.WriteLine($"-> Process by PID: {pID} not found.");
        }
        static void EnumThreadsForPid(int pID)
        {
            Process theProc = GetProcess(pID);
            if (theProc != null)
            {
                // Вывести статистические сведения по каждому потоку в указанном процессе.
                Console.WriteLine("Here are the threads used by: {0}", theProc.ProcessName);
                ProcessThreadCollection theThreads = theProc.Threads;
                foreach (ProcessThread pt in theThreads)
                {
                    string info = $"-> Thread ID: {pt.Id}\t" +
                        $"Start Time: {pt.StartTime.ToShortTimeString()}\t" +
                        $"Priority: {pt.PriorityLevel}";
                    Console.WriteLine(info);
                }
                Console.WriteLine("\n***************************************\n");
            }
            else
                Console.WriteLine($"-> Process by PID: {pID} not found.");
        }
        static void EnumModsForPid(int pID)
        {
            Process theProc = GetProcess(pID);
            if (theProc != null)
            {
                Console.WriteLine("Here are the loaded modules for: {0}", theProc.ProcessName);
                ProcessModuleCollection theMods = theProc.Modules;
                foreach (ProcessModule pm in theMods)
                {
                    string info = $"-> Mod Name: {pm.ModuleName}";
                    Console.WriteLine(info);
                }
                Console.WriteLine("\n***************************************\n");
            }
            else
                Console.WriteLine($"-> Process by PID: {pID} not found.");
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
                //Console.WriteLine(ex.Message);
                return null;
            }
            return theProc;
        }
        static void StartAndKillProcess()
        {
            Process ffProc = null;
            // Запустить Firefox и перейти на сайт https://duckduckgo.com.
            try
            {
                ffProc = Process.Start(@"C:\Program Files\Mozilla Firefox\firefox.exe", "https://duckduckgo.com");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("--> Hit enter to kill {0}...", ffProc.ProcessName);
            Console.ReadLine();
            // Уничтожить процесс Firefox.
            try
            {
                ffProc.Kill();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
