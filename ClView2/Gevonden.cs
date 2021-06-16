using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClView2
{
    public partial class Gevonden : Form
    {
        public string gevonden_text = "";
        private string PreviewFileNaam;
        private readonly RichTextBox clfile = new RichTextBox();
        private bool change_kleur = false;
        private int kleur_regel = 0;
        private readonly string aanvulling = "                                                                                                    "
            + "                                                                                                    ";

        public Gevonden()
        {
            InitializeComponent();
        }

        private void Gevonden_Shown(object sender, EventArgs e)
        {
            Point Point1 = new Point();
            Point1 = DataCL._MainForm.Location;
            Point1.X += 100;
            Point1.Y += 50;

            if (Point1.X < 500)
            {
                Point1.X = 500;
            }

            DataCL._MainForm.Gevonden.Location = Point1;
            DataCL._MainForm.Gevonden.Focus();

        }

        private void listViewGevonden_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // open geselecteerde file, afhankelijk van eb of cl juiste versie.
            string locatie = listViewGevonden.SelectedItems[0].Text.ToString();
            string ext = Path.GetExtension(locatie).ToUpper();
            switch (ext)
            {
                case ".CL":
                //case ".cl":
                    if (listViewGevonden.SelectedItems.Count > 0)
                    {
                        if (DataCL._MainForm.TestOfLaatsteFileVeranderdIs())
                        {
                            // als file geopend is, open deze dan, anders nieuwe tab openen.
                            int geopend = -1;
                            for (int i = 0; i < 5; i++)
                            {
                                if (DataCL._MainForm.GetFileNaamTab(i) == locatie)
                                {
                                    geopend = i;
                                    i = 100;
                                };
                            }
                            if (geopend < 0)
                            {
                                DataCL._MainForm.NieuweFileLaden(locatie);
                                DataCL._MainForm.toolStripComboZoek.Text = gevonden_text;
                                DataCL._MainForm.ZoekEnSelText(this, null);
                                DataCL._MainForm.Focus();
                            }
                            else
                            {
                                // open tab welke al bestaat.
                                DataCL._MainForm.LaadFileViaIndexNummer(geopend + 1);
                                DataCL._MainForm.toolStripComboZoek.Text = gevonden_text;
                                DataCL._MainForm.ZoekFromTop = true;
                                DataCL._MainForm.ZoekEnSelText(this, null);
                                DataCL._MainForm.Focus();

                            }
                        }
                    }
                    break;
                case ".EB":
                    DataCL._EB = new EBForm();
                    DataCL._EB.NieuweEBFile(locatie);
                    DataCL._EB.textBoxZoek.Text = gevonden_text;
                    DataCL._EB.ShowDialog();
                    break;
                //case ".htm":
                case ".CSV":
                case ".PNT":
                case ".TXT":
                case ".XLSX":
                case ".XLS":
                case ".HTM":
                case ".XML":
                    Process.Start(@locatie);
                    break;
                default:
                    break;
            }
        }

        private void Gevonden_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
        }

        private void ListViewGevonden_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = null;
            string ext = " ";
            try
            {
                selected = listViewGevonden.SelectedItems[0].Text.ToString();
                ext = Path.GetExtension(selected).ToUpper();
            }
            catch (Exception) { }


            if (selected == null)
            {
                PrieviewScherm.Clear();
            }

            if (selected != null && PreviewFileNaam != selected && ext == ".CL")
            {
                PreviewFileNaam = selected;
                LaadData(selected, gevonden_text);
            }
        }


        public void LaadData(string FileNaam, string zoek_text)
        {

            string zoek = zoek_text.ToUpper();
            int postext;
            int AantalBloks = 0;

            clfile.Clear();
            PrieviewScherm.Clear();
            PrieviewScherm.WordWrap = false;    // nodig om juiste regels te vinden

            PrieviewScherm.Text = File.ReadAllText(FileNaam);

            postext = PrieviewScherm.Find(zoek, 0, RichTextBoxFinds.None);

            while (postext > 0)
            {
               // if (CheckGeenRemark(postext))
               // {
                    MaakBlok(postext);
                    AantalBloks++;
               // }
                postext = PrieviewScherm.Find(zoek, postext + 1, RichTextBoxFinds.None);
            }

            PrieviewScherm.Clear();
            PrieviewScherm.Text = clfile.Text;


            for (int i = 0; i < AantalBloks * 5; i++)
            {
                MarkLine(i);
            }

            PrieviewScherm.Select(0, 0);
        }

        private bool CheckGeenRemark(int postext)
        {
            bool ret = false;
            int regelnr = PrieviewScherm.GetLineFromCharIndex(postext);
            string RegelString = PrieviewScherm.Lines[regelnr];
            if (RegelString.Substring(0, 2) != "--")
                ret = true;
            return ret;
        }

        private void MaakBlok(int postext)
        {
            int regelnr = PrieviewScherm.GetLineFromCharIndex(postext);
            MaakRegel(regelnr - 2);
            MaakRegel(regelnr - 1);
            MaakRegel(regelnr);
            MaakRegel(regelnr + 1);
            MaakRegel(regelnr + 2);
        }

        private void MaakRegel(int regelnr)
        {
            if (regelnr >= 0)
            {
                string RegelString = PrieviewScherm.Lines[regelnr] + aanvulling;
                clfile.Text += RegelString;
                clfile.Text += Environment.NewLine;
            }
            else
            {
                clfile.Text += aanvulling;
                clfile.Text += Environment.NewLine;
            }
        }

        private void MarkLine(int regel)
        {
            if (kleur_regel > 4)
            {
                kleur_regel = 0;
                change_kleur = !change_kleur;
            }
            kleur_regel++;
            int firstCharOfLineIndex = PrieviewScherm.GetFirstCharIndexFromLine(regel);

            PrieviewScherm.Select(firstCharOfLineIndex, 200);
            if (change_kleur)
            {
                PrieviewScherm.SelectionBackColor = Color.LightSkyBlue;
            }
            else
            {
                PrieviewScherm.SelectionBackColor = Color.LightSteelBlue;
            }
        }
    }
}
