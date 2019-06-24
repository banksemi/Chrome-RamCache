using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ChromeCacheManager
{
    public class Config
    {
        private const string SubKey = "Software\\ChromeCacheManager";

        public Dictionary<string, string> Values = new Dictionary<string, string>();
        public Config()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(SubKey);
            string[] list = rk.GetValueNames();
            foreach (string temp in list)
            {
                string value = (string)rk.GetValue(temp);
                if (value != null)
                    Values[temp] = value;
            }
        }
        public void Save()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(SubKey);
            foreach (string i in Values.Keys)
            {
                rk.SetValue(i, Values[i]);
            }
        }
        public bool Contains(string key)
        {
            return Values.ContainsKey(key);
        }
    }
}
