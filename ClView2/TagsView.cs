using ClView2.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClView2
{
    public partial class TagsView : Form
    {
        private bool Save_Tag_Pos = false;

        public TagsView()
        {
            InitializeComponent();
        }

        public void Zet_Locatie(Point _point)
        {
            this.Location = _point;
        }

        public void Zet_Size(Size _point)
        {
            this.Size = _point;
        }

        private void TagsView_LocationChanged(object sender, EventArgs e)
        {
            if (Save_Tag_Pos)
            {
                Settings.Default.Location_tag_X = Location.X;
                Settings.Default.Location_tag_Y = Location.Y;
            }
        }

        private void TagsView_SizeChanged(object sender, EventArgs e)
        {
            if (Save_Tag_Pos)
            {
                Settings.Default.Size_tag_Width = this.Size.Width;
                Settings.Default.Size_tag_Height = this.Size.Height;
            }
        }

        private void TagsView_Load(object sender, EventArgs e)
        {
            int X = Settings.Default.Location_tag_X;
            int Y = Settings.Default.Location_tag_Y;
            Point Point1 = new Point(X, Y);
            X = Settings.Default.Size_tag_Width;
            Y = Settings.Default.Size_tag_Height;
            Size Size1 = new Size(X, Y);
            this.WindowState = FormWindowState.Normal;
            this.Location = Point1;
            this.Size = Size1;
            Save_Tag_Pos = true;
            load_data_tagview();
        }

        public void load_data_tagview()
        {
            TagView.Items.Clear();
            for (int a = 0; a < DataCL._TagEnBeschrijving.Count; a = a + 2)
            {
                string[] row = { DataCL._TagEnBeschrijving[a], DataCL._TagEnBeschrijving[a + 1] };
                ListViewItem listViewItem = new ListViewItem(row);
                TagView.Items.Add(listViewItem);
            }
        }

        // als in tag scherm klik, dan selecteer in view
        private void TagView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TagView.SelectedItems.Count > 0)
            {
                DataCL._MainForm.toolStripComboZoek.Text = TagView.SelectedItems[0].Text.ToString();
                DataCL._MainForm.ZoekEnSelText(this, null);
            }
            // zet muis op mainform en zet focus
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Point p = new Point(DataCL._MainForm.Location.X + 120, DataCL._MainForm.Location.Y + 280);
            Cursor.Position = p;
        }

        private void TagsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            DataCL._MainForm.tagViewWindowToolStripMenuItem.Checked = false;
            DataCL._MainForm.tagViewWindowToolStripMenuItem_Click(this, null);
        }
    }
}
