using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChromeCacheManager.SchedulerModule;
namespace ChromeCacheManager
{
    public partial class Form1 : Form
    {
        public static Scheduler Scheduler = new ChromeCacheManager.SchedulerModule.WinServiceScheduler();
        public Form1()
        {
            InitializeComponent();
            CacheManagement.InitialSetting();

            foreach (string path in CacheManagement.FindChromeFolder())
            {
                listView1.Items.Add(path);
            }
            Config config = new Config();
            if (config.Contains("path"))
                textBox1.Text = config.Values["path"];

            StateUpdate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.SelectedPath))
            {
                Config config = new Config();
                config.Values["path"] = dialog.SelectedPath + @"\";
                config.Save();
                textBox1.Text = config.Values["path"];
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            CacheManagement.Start();

            string message = "Temporary folder is changed.\n\n" +
                "But the temporary folder will be deleted when you restart.\n" +
                "So, we recommend scheduler registration that works automatically at system startup.\n\n" +
                "Would you like to register?";
            if (Scheduler.isRegistered == false)
            {
                if (MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Scheduler.Register();
                    StateUpdate();
                }
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        { 
            Command.Start(listView1.SelectedItems[0].Text);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Scheduler.Register();
            StateUpdate();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            Scheduler.Deregister();
            StateUpdate();
        }
        public void StateUpdate()
        {
            string text = null;
            if (Scheduler.isRegistered)
            {
                text = "Registered";
                button2.Enabled = false;
                button3.Enabled = true;
            }
            else
            {
                text = "Unregistered";
                button2.Enabled = true;
                button3.Enabled = false;
            }

            label3.Text = "State (" + text + ")";
        }
    }
}
