using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeCacheManager.SchedulerModule
{
    public abstract class Scheduler
    {
        public bool isRegistered
        {
            get
            {
                return onIsRegistered();
            }
        }
        protected abstract bool onIsRegistered();
        public abstract void Register();
        public abstract void Deregister();
    }
}
