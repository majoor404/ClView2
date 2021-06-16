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
    public partial class StatementForm : Form
    {
        public StatementForm()
        {
            InitializeComponent();
        }

        private void StatementForm_Shown(object sender, EventArgs e)
        {
            Point Point1 = new Point();
            Point1 = DataCL._MainForm.Location;
            Point1.X += 10;
            Point1.Y += 10;

            this.Location = Point1;
            Size = DataCL._MainForm.Size;
        }
    }
}
