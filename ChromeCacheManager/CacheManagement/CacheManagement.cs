using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CacheManagementModule
{
    public static class CacheManagement
    {

        public static bool TempFileSystem = true;

        private static string LocalFolder = null;
        private static string RamDiskFolder = null;
        private static void StartTemp(string ori)
        {
            string temp = ori.Replace(LocalFolder, "LocalFolder");

            // Remove unusable characters
            temp = temp.Replace("%", "-");
            temp = temp.Replace(":", "+");
            temp = temp.Replace("\\", "_");
            temp = RamDiskFolder + temp;
            Directory.CreateDirectory(temp);
            Command.Execute("rd /s /q " + "\"" + ori + '"');
            Command.Execute("mklink /j " + "\"" + ori + '"' + " \"" + temp + "\"");
        }
        public static string[] FindChromeFolder()
        {
            LocalFolder = new Config().Values["LocalFolder"];
            RamDiskFolder = new Config().Values["path"];

            List<string> ChromeFolder = new List<string>();
             string[] temp;

            temp = Directory.GetDirectories(LocalFolder + @"\Google\Chrome\User Data", "*Cache*", SearchOption.AllDirectories);
            ChromeFolder.AddRange(temp);

            if (TempFileSystem == true)
            {
                temp = Directory.GetDirectories(LocalFolder + @"\Google\Chrome\User Data", "File System", SearchOption.AllDirectories);
                ChromeFolder.AddRange(temp);
            }

            return ChromeFolder.ToArray();
        }
        public static void Start()
        {
            LocalFolder = new Config().Values["LocalFolder"];
            RamDiskFolder = new Config().Values["path"];
            if (RamDiskFolder == null)
            {
                throw new Exception("Error");
            }
            foreach (string path in FindChromeFolder())
            {
                StartTemp(path);
            }
        }
        public static void InitialSetting(string RamDiskFolder)
        {
            Config config = new Config();
            config.Values["LocalFolder"] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            config.Values["path"] = RamDiskFolder;
            config.Save();
        }
    }
}
