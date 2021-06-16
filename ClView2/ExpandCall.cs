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
    public partial class ExpandCall : Form
    {
        public ExpandCall()
        {
            InitializeComponent();
            DataCL._ExpandView = richTextBoxExpandCall;
        }

        private void ExpandCall_Shown(object sender, EventArgs e)
        {
            textBoxHint.Text = "";
        }

        private void richTextBoxExpandCall_MouseUp(object sender, MouseEventArgs e)
        {
                if (richTextBoxExpandCall.SelectedText.Length > 0 )
                {
                // haal uit tag alle data, eerst zoeken
                DataCL._ViewTools.ZetSelectieMooi(richTextBoxExpandCall);
                for (int a = 0; a < DataCL._TagEnBeschrijving.Count; a = a + 2)
                    {
                        if (richTextBoxExpandCall.SelectedText == DataCL._TagEnBeschrijving[a])
                        {
                            textBoxHint.Text = DataCL._TagEnBeschrijving[a + 1];

                            break;
                        }
                    }
                }
                else
                {
                    textBoxHint.Text = "";
                }
        }
    }
}
