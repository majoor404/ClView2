using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClView2
{
    public partial class OpZoekNaarForm : Form
    {
        public OpZoekNaarForm()
        {
            InitializeComponent();
        }

        private void OpZoekNaarForm_Shown(object sender, EventArgs e)
        {
            textBoxZoek.Focus();
        }

        public void ZoekVulText(string text)
        {
            textBoxZoek.Text = text;
            textBoxZoek.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // dummy click voor f3 
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F3:
                    button1_Click(this, null);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataCL._MainForm.ZoekFromTop = checkBox1.Checked;
        }
    }
}
