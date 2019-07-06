using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CacheManagementModule
{
    public static class Command
    {
        public static string Execute(string line)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.FileName = "cmd";
            info.RedirectStandardOutput = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = info;
            process.Start();

            process.StandardInput.WriteLine(line);
            process.StandardInput.Close();


            process.WaitForExit();

            string result = process.StandardOutput.ReadToEnd();
            process.Close();
            return result;
        }
        public static void Start(string path)
        {
            Process.Start(path);
        }
    }
}
