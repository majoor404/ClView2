using ClView2.Properties;
using System;
using System.Windows.Forms;

namespace ClView2
{
    public partial class DirectoryKeuze : Form
    {
        public DirectoryKeuze()
        {
            InitializeComponent();
            DataCL.Temp = "c:\\";
        }

        private void c21_Click_1(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Conv 21 CL locatie");
        }

        private void c22_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Conv 22 CL locatie");
        }

        private void c23_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Conv 23 CL locatie");
        }

        private void alg_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Algemeen CL locatie");
        }

        private void c21O_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Ontwikkel conv 21 CL locatie");
        }

        private void c22O_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Ontwikkel conv 22 CL locatie");
        }

        private void C23O_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Ontwikkel conv 23 CL locatie");
        }

        private void algO_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Ontwikkel algemeen CL locatie");
        }

        private void eb_Click(object sender, EventArgs e)
        {
            DataCL.Temp = DataCL.AlgIniFile.Read("Eb locatie");
        }

        private void eigen_Click(object sender, EventArgs e)
        {
            DataCL.Temp = Settings.Default.Eigen_Opslag_CL_files;
        }
    }
}
