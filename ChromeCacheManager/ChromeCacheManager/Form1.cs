using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromeCacheManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            CacheManagement.AddScheduler();
            StateUpdate();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            CacheManagement.RemoveScheduler();
            StateUpdate();
        }
        public void StateUpdate()
        {
            string text = null;
            if (CacheManagement.Registered)
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
