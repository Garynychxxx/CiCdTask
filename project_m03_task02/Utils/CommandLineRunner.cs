using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace project_m03_task02.Utils
{
    class CommandLineRunner
    {        
        private static Process process;

        public static void ExecuteScript()
        {
            process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
                        
            var currTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss.f");
            var dest = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\allure-report_{currTime}";
            Directory.CreateDirectory(dest);

            process.Start();
            process.StandardInput.WriteLine($"allure generate allure-results -o {dest} && allure open {dest}");
            process.StandardInput.Close();
        }
    }
}
