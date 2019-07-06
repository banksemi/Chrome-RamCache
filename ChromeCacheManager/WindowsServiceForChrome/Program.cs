using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
namespace WindowsServiceForChrome
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
            else if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-install":
                        //InstallService();
                        //StartService();
                        break;
                    case "-uninstall":
                       // StopService();
                        //UninstallService();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
