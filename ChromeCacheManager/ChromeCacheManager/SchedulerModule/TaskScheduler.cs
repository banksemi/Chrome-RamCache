using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

using System.Windows.Forms;
namespace ChromeCacheManager.SchedulerModule
{
    public class TaskScheduler : Scheduler
    {
        private const string SchedulerName = "Setting Chrome RamDisk";
        public override void Deregister()
        {
            if (isRegistered)
            {
                TaskService ts = new TaskService();
                ts.RootFolder.DeleteTask(SchedulerName);
                MessageBox.Show("Success", SchedulerName);
            }
        }

        public override void Register()
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
        }

        protected override bool onIsRegistered()
        {
            TaskService ts = new TaskService();
            return ts.GetTask(SchedulerName) != null;
        }
    }
}
