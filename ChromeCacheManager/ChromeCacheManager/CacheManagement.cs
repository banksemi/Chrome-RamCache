using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ChromeCacheManager
{
    public static class CacheManagement
    {
        private static string RamDisk_Temp = @"X:\TEMP\";
        private static void StartTemp(string ori)
        {
            string temp = ori.Replace("%", "-");
            temp = temp.Replace("\\", "_");
            temp = RamDisk_Temp + temp;
            Directory.CreateDirectory(temp);
            Command.Execute("rd /s /q " + "\"" + ori + '"');
            Command.Execute("mklink /j " + "\"" + ori + '"' + " \"" + temp + "\"");
        }
        public static void Start()
        {
            StartTemp(@"%LocalAppData%\Google\Chrome\User Data\Default\Cache");
            StartTemp(@"%LocalAppData%\Google\Chrome\User Data\Default\Code Cache");
            StartTemp(@"%LocalAppData%\Google\Chrome\User Data\Default\GPUCache");
            StartTemp(@"%LocalAppData%\Google\Chrome\User Data\Default\File System");
            StartTemp(@"%LocalAppData%\Google\Chrome\User Data\ShaderCache");
        }
    }
}
