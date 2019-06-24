using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
namespace ChromeCacheManager
{
    public static class CacheManagement
    {
        private const string SchedulerName = "Setting Chrome RamDisk";

        public static bool TempFileSystem = true;

        private static string LocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string RamDiskFolder = null;

        public static bool Registered
        {
            get
            {
                TaskService ts = new TaskService();
                return ts.GetTask(SchedulerName) != null;
            }
        }
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
            RamDiskFolder = new Config().Values["path"];
            if (RamDiskFolder == null)
            {
                MessageBox.Show("Select Ramdisk Temporary Folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (string path in FindChromeFolder())
            {
                StartTemp(path);
            }
        }
        public static void AddScheduler()
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Automatically changes Temporary folder of Chrome on Windows startup.";

            td.Principal.UserId = string.Concat(Environment.UserDomainName, "\\", Environment.UserName); 
            td.Principal.LogonType = TaskLogonType.S4U;
            td.Principal.RunLevel = TaskRunLevel.Highest;

            BootTrigger bt = new BootTrigger();
            td.Triggers.Add(bt);

   
            td.Actions.Add(new ExecAction(Application.ExecutablePath, "/auto", null));

            ts.RootFolder.RegisterTaskDefinition("Setting Chrome RamDisk", td);

            MessageBox.Show("Success", SchedulerName);
        }
        public static void RemoveScheduler()
        {
            if (Registered)
            {
                TaskService ts = new TaskService();
                ts.RootFolder.DeleteTask(SchedulerName);
                MessageBox.Show("Success", SchedulerName);
            }
        }
    }
}
