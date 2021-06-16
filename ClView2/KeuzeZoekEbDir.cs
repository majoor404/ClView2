using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClView2
{
    public partial class KeuzeZoekEbDir : Form
    {
        public KeuzeZoekEbDir()
        {
            InitializeComponent();

            // zorgt voor verticale layout
            listViewKeuzeDir.Scrollable = true;
            listViewKeuzeDir.View = View.Details;
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "col1";
            listViewKeuzeDir.Columns.Add(header);
            listViewKeuzeDir.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewKeuzeDir.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void KeuzeZoekEbDir_Shown(object sender, EventArgs e)
        {
            // laad directory lijst
            listViewKeuzeDir.Items.Clear();

            var directories = Directory.GetDirectories(DataCL.AlgIniFile.Read("Eb locatie")).OrderBy(d => new FileInfo(d).Name);

            foreach (var dir in directories)
            {
                var listItem = new ListViewItem(dir);
                listViewKeuzeDir.Items.Add(listItem);
            }
            // select laatste
            int aantal = listViewKeuzeDir.Items.Count;
            listViewKeuzeDir.Items[aantal-1].Selected = true;
            listViewKeuzeDir.Items[aantal-1].Focused = true;
            listViewKeuzeDir.EnsureVisible(listViewKeuzeDir.FocusedItem.Index);
            listViewKeuzeDir.Focus();
        }
    }
}
