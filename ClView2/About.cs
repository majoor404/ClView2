using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClView2
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            labelCompileDatum.Text = buildDate.ToLongDateString() + " " + buildDate.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open foto clview.jpeg
            Process.Start(@"clview.jpg");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ex = new ExpandCall();
            ex.Text = "Veranderingen aan programma";
            DataCL._ExpandView.Text = File.ReadAllText(@"log.txt");
            ex.ShowDialog();
        }

        private void About_Shown(object sender, EventArgs e)
        {
            textBoxInfo.Clear();
            textBoxInfo.AppendText("Debug data voor majoor ;-)" + "\r\n");
            textBoxInfo.AppendText( "Locatie Tabs Ini = " + DataCL.TabsIniFile.Path + "\r\n");
            textBoxInfo.AppendText("Locatie Algemeen Ini = " + DataCL.AlgIniFile.Path + "\r\n");
            textBoxInfo.AppendText("opslag data %userprofile%\appdata\\local or %userprofile%\\Local Settings\\Application Data");
        }
    }
}
