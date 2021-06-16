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
    public partial class SaveNaarForm : Form
    {
        public string save_locatie;

        public SaveNaarForm()
        {
            InitializeComponent();
        }

        private void buttonAlg_Click(object sender, EventArgs e)
        {
            Button but = (Button) sender;

            switch (but.Text)
            {
                case "Ontwikkeling ALG":
                    save_locatie = DataCL.AlgIniFile.Read("Ontwikkel algemeen CL locatie");
                    break;
                case "Ontwikkeling 21":
                    save_locatie = DataCL.AlgIniFile.Read("Ontwikkel conv 21 CL locatie");
                    break;
                case "Ontwikkeling 22":
                    save_locatie = DataCL.AlgIniFile.Read("Ontwikkel conv 22 CL locatie");
                    break;
                case "Ontwikkeling 23":
                    save_locatie = DataCL.AlgIniFile.Read("Ontwikkel conv 23 CL locatie");
                    break;
                default:
                    save_locatie = "C:\\";
                    break;
            }

        }
    }
}
