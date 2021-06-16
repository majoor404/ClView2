using ClView2.Properties;
using System.Windows.Forms;

namespace ClView2
{
    public partial class Instellingen : Form
    {
        public Instellingen()
        {
            InitializeComponent();
            
        }

        private void listBoxLocaties_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // item geselecteerd
            string keuze = listBoxLocaties.SelectedItem.ToString();

            if (keuze != "Eigen Opslag CL files")
            {

                textBoxLocatie.Text = DataCL.AlgIniFile.Read(keuze);

                if (DataCL.AlgIniFile.Read(keuze + " Recursion") == "True")
                {
                    checkBoxOnderligend.Checked = true;
                }
                else
                {
                    checkBoxOnderligend.Checked = false;
                }
                buttonChange.Enabled = listBoxLocaties.SelectedIndex > -1;
            }
            else
            {
                textBoxLocatie.Text = Settings.Default.Eigen_Opslag_CL_files;

                if (Settings.Default.Eigen_Opslag_CL_files_Recursion)
                {
                    checkBoxOnderligend.Checked = true;
                }
                else
                {
                    checkBoxOnderligend.Checked = false;
                }
                buttonChange.Enabled = listBoxLocaties.SelectedIndex > -1;
            }
        }

        private void buttonChange_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxLocatie.Text = folderBrowserDialog1.SelectedPath;
                save();
            }
        }

        private void checkBoxOnderligend_CheckedChanged(object sender, System.EventArgs e)
        {
           
        }

        private void checkBoxOnderligend_Click(object sender, System.EventArgs e)
        {
            save();
        }

        private void save()
        {
            if (listBoxLocaties.SelectedItem.ToString() != "Eigen Opslag CL files")
            {
                // algemene data in ini file in directory clview
                DataCL.AlgIniFile.Write(listBoxLocaties.SelectedItem.ToString(), textBoxLocatie.Text);
                DataCL.AlgIniFile.Write(listBoxLocaties.SelectedItem.ToString() + " Recursion", checkBoxOnderligend.Checked.ToString());

            }
            else
            {
                // eigen data in %userprofile%\appdata\local
                Settings.Default.Eigen_Opslag_CL_files = textBoxLocatie.Text;
                Settings.Default.Eigen_Opslag_CL_files_Recursion = checkBoxOnderligend.Checked;
            }
        }
    }
}
