using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace ChromeCacheManager
{
    public static class CacheManagement
    {
        public static bool TempFileSystem = true;

        private static string LocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string RamDiskFolder = null;
        private static List<string> ChromeFolder = new List<string>();
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
            RamDiskFolder = new Config().Values["path"];
            if (RamDiskFolder == null)
            {
                MessageBox.Show("Select Ramdisk Temporary Folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindChromeFolder();
            foreach (string path in ChromeFolder)
            {
                StartTemp(path);
            }
        }
    }
}
