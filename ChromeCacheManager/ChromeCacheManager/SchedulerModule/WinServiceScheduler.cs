using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsServiceForChrome.Management;

namespace ChromeCacheManager.SchedulerModule
{
    public class WinServiceScheduler : Scheduler
    {
        public override void Deregister()
        {
            ServiceManager.UninstallService();
        }

        public override void Register()
        {
            ServiceManager.InstallService();
        }

        protected override bool onIsRegistered()
        {
            return ServiceManager.IsInstalled();
        }
    }
}
