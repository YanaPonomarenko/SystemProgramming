using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SystemProgramming;

internal class ProcessDemo
{
    private Dictionary<string, Process> myProcesses = new Dictionary<string, Process>();
    public void Run()
    {
        ConsoleKeyInfo key;
        do
        {
            Console.WriteLine("Process Demo");
            Console.WriteLine("1 - ShowAllProcessesFilter");
            Console.WriteLine("2- ShowAllProcesses");
            Console.WriteLine("3 - GetProcessByPid");
            Console.WriteLine("4 - CreateProcess");
            Console.WriteLine("5 - KillProcess");
            Console.WriteLine("6 - CallTestProgramm");
            Console.WriteLine("0 - Exist");
            key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case '1':
                    ShowAllProcessesFilter();
                    break;
                case '2':
                    ShowAllProcesses();
                    break;
                case '3':
                    GetProcessByPid();
                    break;
                case '4':
                    CreateProcess();
                    break;
                case '5':
                    KillProcess();
                    break;
                case '6':
                    CallTestProgramm();
                    break;
                default:
                    Console.WriteLine("unknown operation");
                    break;

            }
        } while (key.KeyChar != '0');


    }
    private void GetProcessByPid()
    {
        try
        {
            Console.WriteLine("Enter pid:");
            int pid = Convert.ToInt32(Console.ReadLine());
            var process = Process.GetProcessById(pid);
            Console.WriteLine($"{process.ProcessName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    private void ShowAllProcessesFilter()
    {
        Process[] processes = Process.GetProcesses();
        Dictionary<String, int> taskManager = new Dictionary<String, int>();
        foreach (var process in processes)
        {
            string processName = string.Empty;
            try
            {
                processName = process.ProcessName;
            }
            catch (Exception)
            {
                processName = "unknown";
            }
            if (taskManager.ContainsKey(processName))
            {
                taskManager[processName] += 1;
            }
            else
            {
                taskManager[processName] = 1;
            }
        }
        foreach (var process in taskManager)
        {
            Console.WriteLine($"{process.Key} {process.Value}");
        }
    }
    private void ShowAllProcesses()
    {
        Process[] processes = Process.GetProcesses();

        foreach (var process in processes)
        {
            Console.WriteLine($"{process.ProcessName} PID: {process.Id}");
        }
    }
    private void CreateProcess()
    {
        Console.WriteLine("Enter programm name: ");
        string? programm = Console.ReadLine();
        if (programm != null && programm != "")
        {
            if (myProcesses.ContainsKey(programm))
            {
                Console.WriteLine($"Process {programm} already running");
                return;
            }

            try
            {
                myProcesses[programm] = Process.Start(programm);
                Console.WriteLine($"Process started");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }
    }
    private void KillProcess()
    {
        Console.Write("Enter PID to kill: ");
        if (int.TryParse(Console.ReadLine(), out int pid))
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                process.Kill();
                Console.WriteLine($"Process {process.ProcessName} killed");

                foreach (var item in myProcesses)
                {
                    if (item.Value.Id == pid)
                    {
                        myProcesses.Remove(item.Key);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }
    }

    private void CallTestProgramm()
    {
        string exePath = @"C:\Users\ypono\source\repos\SystemProgramming\TestProgramm\bin\Debug\net8.0\TestProgramm.exe";
        Console.WriteLine("Enter arg(hi,bye,etc...)");
        string arg = Console.ReadLine()??"hi";
        ProcessStartInfo processInfo = new ProcessStartInfo()
        {
            FileName = exePath,
            Arguments = arg,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        using (Process process = new Process())
        {
            process.StartInfo = processInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();
            process.WaitForExit();//завершення процеса
            if(string.IsNullOrEmpty(errors))
            {
                Console.WriteLine($"Result: {output}");
            }
            else
            {
                Console.WriteLine($"Error: {errors}");
            }
                //Console.WriteLine($"Result: {output}");
        };
        
    }
}
