using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ChromeCacheManager;
namespace WindowsServiceForChrome
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                CacheManagement.Start();
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("c:\\t.txt", e.Message + e.StackTrace);
            }
            Stop();
        }

        protected override void OnStop()
        {
        }
    }
}
