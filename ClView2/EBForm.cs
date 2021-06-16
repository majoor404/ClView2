using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClView2
{
    public partial class EBForm : Form
    {
        private IniFile HulpIniEbfile;
        private bool eerste_keer_geopend;

        public EBForm()
        {
            InitializeComponent();
        }

        public void NieuweEBFile(string filenaam)
        {
            HulpIniEbfile = new IniFile("HulpEb.ini"); // zelfde dir als programma

            textBox2.Text = filenaam;

            //textBoxEB.Text = File.ReadAllText(@filenaam, Encoding.ASCII);

            // EndOfStream / EndOfFile;

            //char ch;
            //StreamReader reader;
            //StringBuilder t = new StringBuilder();
            //reader = new StreamReader(@filenaam);
            //do
            //{
            //    ch = (char)reader.Read();
            //    t.Append(ch);
            //} while (!reader.EndOfStream);
            //reader.Close();
            //reader.Dispose();
            //textBoxEB.Text = t.ToString();


            //StringBuilder t = new StringBuilder();
            //using (StreamReader reader = new StreamReader(filenaam))
            //{
            //    while (true)
            //    {
            //        string line = reader.ReadLine();
            //        if (line == null)
            //        {
            //            break;
            //        }
            //        t.Append(line);
            //    }
            //}
            //textBoxEB.Text = t.ToString();

            //StreamReader srRead = new StreamReader(filenaam);
            //string strFileText = "";
            //strFileText = srRead.ReadToEnd();
            //srRead.Close();
            //textBoxEB.Text = strFileText;

            listView.Items.Clear();
            var lines = new List<String> ();

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(filenaam))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // in eb zitten soms helaas wat controll char waardoor file niet
                    // geheel geladen werdt.
                    line = StripHighBitCharacters(line);
                    lines.Add(line);
                    

                    if (line.Length > 15)
                    {
                        string test = line.Substring(0, 15);
                        if (test == "{SYSTEM ENTITY ")
                        {
                            test = line.Substring(15);
                            int POS = test.IndexOf('(');
                            test = test.Substring(0, POS);
                            var listViewItem = new ListViewItem(test);
                            listView.Items.Add(listViewItem);
                        }
                    }
                }
            }
            listView.SelectedItems.Clear();
            textBoxEB.Lines = lines.ToArray();
        }

        public string StripHighBitCharacters(string value)
        {
            StringBuilder line = new StringBuilder();
            foreach (var c in value.Where(c => c > 31 && c < 176))
            {
                line.Append(c);
            }
            return line.ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonFullEB_Click(object sender, EventArgs e)
        {
            textBoxEB.Visible = !textBoxEB.Visible;
            textBoxEBDeel.Visible = !textBoxEBDeel.Visible;
            panelHulp.Visible = !panelHulp.Visible;
            textBoxEB.SelectionStart = 0;
            textBoxEB.SelectionLength = 0;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                textBoxEBDeel.Clear();
                string OpZoek = "{SYSTEM ENTITY " + listView.SelectedItems[0].Text.ToString();
                int pos = textBoxEB.Text.IndexOf(OpZoek);
                if (pos > -1)
                {
                    int eind = textBoxEB.Text.IndexOf("{SYSTEM ENTITY", pos + 1);

                    if (eind < 0)
                        eind = textBoxEB.Text.Count();

                    textBoxEB.SelectionStart = pos;
                    textBoxEB.SelectionLength = eind - pos;
                    textBoxEB.Copy();
                    textBoxEBDeel.Paste();
                    textBoxEBDeel.SelectionStart = 1;
                    textBoxEBDeel.ScrollToCaret();
                }

                textBoxTagNaam.Text = VulDummyData("&N ");
                textBoxDesc.Text = VulDummyData("KEYWORD  =\"");
                textBoxPointDesc.Text = VulDummyData("PTDESC   =\"");
                textBoxSoortTag.Text = VulDummyData("&T ");

                textBoxNetwerkNummer.Text = VulDummyData("NTWKNUM  = ");
                textBoxTypeNode.Text = VulDummyData("NODETYP  =");
                textBoxNodeNummer.Text = VulDummyData("NODENUM  =");

                textBoxModuleNummer.Text = VulDummyData("MODNUM   = ");
                textBoxSlotNummer.Text = VulDummyData("SLOTNUM  =");
                // Locatie

                // uitleg locatie netwerk
                textBoxUitlegLocatieNetwerk.Text = HulpIniEbfile.Read(textBoxNetwerkNummer.Text.ToString());
                // uitleg soort tag.
                textBoxSoortTagVertaling.Text = HulpIniEbfile.Read(textBoxSoortTag.Text.ToString());
                // hulp tag voor tekening nummer
                textBoxTekeningNummer.Text = HulpIniEbfile.Read(textBoxNetwerkNummer.Text + textBoxTypeNode.Text + textBoxNodeNummer.Text + "Tekening");
                // locatie box
                textBoxLocatie.Text = HulpIniEbfile.Read(textBoxNetwerkNummer.Text + textBoxTypeNode.Text + textBoxNodeNummer.Text + "Locatie");
            }
        }

        private void buttonZoek_Click(object sender, EventArgs e)
        {
            // op zoek naar textBoxZoek.Text;
            // afhankelijk of grote eb file zichtbaar is, 2 mogelijkheden
            if (!textBoxEB.Visible)
            {
                listView.Focus();
                int huidig = -1;
                int a;
                if (listView.FocusedItem != null)
                    huidig = listView.FocusedItem.Index;
                if (eerste_keer_geopend)
                {
                    huidig--;
                    eerste_keer_geopend = false;
                }
                textBoxZoek.Text = textBoxZoek.Text.ToUpper();
                if (textBoxZoek.Text.Length > 0)
                {
                    for (a = huidig + 1; a < listView.Items.Count; a++)
                    {
                        if (a < listView.Items.Count)
                        {
                            string tag_ = listView.Items[a].Text.ToString();
                            if (tag_.IndexOf(textBoxZoek.Text) > -1)
                            {
                                listView.Items[a].Selected = true;
                                listView.Items[a].Focused = true;
                                break;
                            }
                        }
                    }
                }
                try
                {
                    listView.EnsureVisible(listView.FocusedItem.Index);
                }
                catch { }
                listView.Focus();
            }
            else
            {
                // zoek in grote eb file
                int pos = textBoxEB.SelectionStart;
                pos = textBoxEB.Text.IndexOf(textBoxZoek.Text.ToUpper(), pos+1);
                if(pos>0)
                {
                    textBoxEB.SelectionStart = pos;
                    textBoxEB.SelectionLength = textBoxZoek.Text.Length;
                    textBoxEB.ScrollToCaret();
                }

            }
        }

        private string VulDummyData(string zoek)
        {
            string Ret = "";
            string regel = "";
            int pos;
            for (int a = 0; a < textBoxEBDeel.Lines.Count(); a++)
            {
                regel = textBoxEBDeel.Lines[a];
                pos = regel.IndexOf(zoek);
                if (pos > -1)
                {
                    regel = regel.Substring(pos + zoek.Length);
                    pos = regel.IndexOf("\"");
                    if (pos > -1)
                        regel = regel.Substring(0, pos);
                    Ret = regel.Trim();
                    break;
                }
            }
            return Ret;
        }

        private void textBoxUitlegLocatieNetwerk_TextChanged(object sender, EventArgs e)
        {
            HulpIniEbfile.Write(textBoxNetwerkNummer.Text.ToString(), textBoxUitlegLocatieNetwerk.Text);
        }

        private void textBoxSoortTagVertaling_TextChanged(object sender, EventArgs e)
        {
            HulpIniEbfile.Write(textBoxSoortTag.Text.ToString(), textBoxSoortTagVertaling.Text);
        }

        private void textBoxTekeningNummer_TextChanged(object sender, EventArgs e)
        {
            HulpIniEbfile.Write(textBoxNetwerkNummer.Text + textBoxTypeNode.Text + textBoxNodeNummer.Text + "Tekening", textBoxTekeningNummer.Text);
        }

        // op zoek naar textBoxZoek.Text up richting
        private void button1_Click(object sender, EventArgs e)
        {
            // op zoek naar textBoxZoek.Text;
            // afhankelijk of grote eb file zichtbaar is, 2 mogelijkheden
            if (!textBoxEB.Visible)
            {
                listView.Focus();
                int huidig = listView.Items.Count - 1;
                int a;
                if (listView.FocusedItem != null)
                    huidig = listView.FocusedItem.Index;

                textBoxZoek.Text = textBoxZoek.Text.ToUpper();
                if (textBoxZoek.Text.Length > 0)
                {
                    for (a = huidig - 1; a > -1; a--)
                    {
                        if (a > -1)
                        {
                            string tag_ = listView.Items[a].Text.ToString();
                            if (tag_.IndexOf(textBoxZoek.Text) > -1)
                            {
                                listView.Items[a].Selected = true;
                                listView.Items[a].Focused = true;
                                break;
                            }
                        }
                    }
                    listView.EnsureVisible(listView.FocusedItem.Index);
                    listView.Focus();
                }
            }
            else
            {
                // zoek op in grote eb file
                int pos = textBoxEB.SelectionStart;
                pos = textBoxEB.Text.LastIndexOf(textBoxZoek.Text.ToUpper(), pos);
                if (pos > 0)
                {
                    textBoxEB.SelectionStart = pos;
                    textBoxEB.SelectionLength = textBoxZoek.Text.Length;
                    textBoxEB.ScrollToCaret();
                }


            }
        }

        private void textBoxLocatie_TextChanged(object sender, EventArgs e)
        {
            HulpIniEbfile.Write(textBoxNetwerkNummer.Text + textBoxTypeNode.Text + textBoxNodeNummer.Text + "Locatie", textBoxLocatie.Text);
        }

        private void EBForm_Shown(object sender, EventArgs e)
        {
            // als er data staat in zoek venster, dan daar heen springen
            // is ingevuld bij aanroep vanuit gevonden scherm
            if (textBoxZoek.Text.Length > 0)
            {
                eerste_keer_geopend = true; // moet dit weten, anders selecteerd die niet tag als die helemaal
                                            // boven in staat (is default in listview)
                buttonZoek_Click(this, null);
            }
        }

        // als zoek text bestaat uit minimaal 5 char, zoek dan /*op de achtergrond*/ of er ook een refentie 
        // bestaat met deze zoektekst
        private void textBoxZoek_TextChanged(object sender, EventArgs e)
        {
            buttonHelpRef.Visible = false;
            labelReference.Visible = false;
            if (textBoxZoek.Text.Length > 4)
            {
                string zoek = textBoxZoek.Text.ToUpper();
                string eerste_mogelijkheid;
                string tweede_mogelijkheid;

                int aantal_regels = textBoxEB.Lines.Length;

                int pos = textBoxEB.Text.IndexOf(zoek);

                // 2 mogelijkheden
                // eerste is dat er voor staat : {SYSTEM ENTITY 1_O2_PER( ) 
                // tweede is : &N 1_O2_PER

                if (pos > 0)
                {
                    while (pos > 0)
                    {
                        eerste_mogelijkheid = textBoxEB.Text.Substring(pos - 15, 14);
                        tweede_mogelijkheid = textBoxEB.Text.Substring(pos - 3, 3);
                        if (!(eerste_mogelijkheid == "{SYSTEM ENTITY" || tweede_mogelijkheid == "&N "))
                        {
                            buttonHelpRef.Visible = true;
                            labelReference.Visible = true;
                            break;
                        }
                        pos = textBoxEB.Text.IndexOf(zoek, pos + 1);
                    }
                }
            }
        }

        // help knop reference gevonden.
        private void buttonHelpRef_Click(object sender, EventArgs e)
        {
            // maak eerst help tekst.
            StringBuilder help = new StringBuilder();
            help.Append("Als een reference gevonden is, betekend dat" + Environment.NewLine);
            help.Append("gezochte tekst : " + textBoxZoek.Text.ToUpper() + " in deze eb-file" + Environment.NewLine);
            help.Append("gebruikt wordt door een andere tag." + Environment.NewLine);
            help.Append(Environment.NewLine);
            help.Append("Om deze te vinden schakel over naar voledige EB view" + Environment.NewLine);

            MessageBox.Show(help.ToString());
        }
    }
}
