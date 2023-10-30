using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClView2
{
    public partial class ZoekNaarTags : Form
    {
        private bool IsCancelled;
        private const int BufferSize = 128;
        private string[] fileEntries;

        public ZoekNaarTags()
        {
            InitializeComponent();
            treeView1.ExpandAll();

        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool LockWindowUpdate(IntPtr hWndLock);

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                buttonZoekString_Click(this, null);
                return true;
            }
            if (keyData == Keys.F4)
            {
                CancelBut_Click(this, null);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonZoekString_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            string gekozen = node.Text;
            if (gekozen == "CL-file's")
            {
                var result = MessageBox.Show("Op zoek in ALLE locatie's ?,dit duurt even", "Vraagje", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    gekozen = "";
                }

            }

            LockWindowUpdate(treeView1.Handle);
            DataCL._MainForm.FileNaamStatusStrip.Text = "Zoek tekst in file's";
            InfoLabel.Text = "Zoek tekst in file's";
            Refresh();
            DataCL._MainForm.Gevonden.listViewGevonden.Items.Clear();

            InfoLabel.Text = string.Format("op zoek in selected: {0}", node.Text);
            Refresh();

            IsCancelled = false;
            switch (gekozen)
            {
                case "Algemeen":
                    Zoek("Algemeen CL locatie", "*.CL");
                    Zoek("Ontwikkel algemeen CL locatie", "*.CL");
                    break;
                case "Algemeen CL locatie":
                    Zoek("Algemeen CL locatie", "*.CL");
                    break;
                case "Ontwikkel algemeen CL locatie":
                    Zoek("Ontwikkel algemeen CL locatie", "*.CL");
                    break;
                case "Historie algemeen CL locatie":
                    Zoek("Historie algemeen CL locatie", "*.CL");
                    break;
                case "Eb locatie":
                    Zoek("Eb locatie", "*.EB");
                    //Zoek("Eb locatie", "*.XML");
                    break;
                //case "OPC data locatie":
                //    Zoek("OPC data locatie", "*.CSV");
                //    break;
                case "C21":
                    Zoek("Conv 21 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 21 CL locatie", "*.CL");
                    break;
                case "Conv 21 CL locatie":
                    Zoek("Conv 21 CL locatie", "*.CL");
                    break;
                case "Ontwikkel conv 21 CL locatie":
                    Zoek("Ontwikkel conv 21 CL locatie", "*.CL");
                    break;
                case "Historie conv 21 CL locatie":
                    Zoek("Historie conv 21 CL locatie", "*.CL");
                    break;
                case "C22":
                    Zoek("Conv 22 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 22 CL locatie", "*.CL");
                    break;
                case "Conv 22 CL locatie":
                    Zoek("Conv 22 CL locatie", "*.CL");
                    break;
                case "Ontwikkel conv 22 CL locatie":
                    Zoek("Ontwikkel conv 22 CL locatie", "*.CL");
                    break;
                case "Historie conv 22 CL locatie":
                    Zoek("Historie conv 22 CL locatie", "*.CL");
                    break;
                case "C23":
                    Zoek("Conv 23 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 23 CL locatie", "*.CL");
                    break;
                case "Conv 23 CL locatie":
                    Zoek("Conv 23 CL locatie", "*.CL");
                    break;
                case "Ontwikkel conv 23 CL locatie":
                    Zoek("Ontwikkel conv 23 CL locatie", "*.CL");
                    break;
                case "Historie conv 23 CL locatie":
                    Zoek("Historie conv 23 CL locatie", "*.CL");
                    break;
                case "Historie":
                    Zoek("Historie algemeen CL locatie", "*.CL");
                    Zoek("Historie conv 21 CL locatie", "*.CL");
                    Zoek("Historie conv 22 CL locatie", "*.CL");
                    Zoek("Historie conv 23 CL locatie", "*.CL");
                    break;
                case "Scada pnt locatie":
                    Zoek("Scada punten locatie", "*.pnt");
                    break;
                case "Experion C300 locatie":
                    Zoek("Experion C300 locatie", "*.*");
                    break;
                case "Display locatie":
                    Zoek("Display locatie", "*.htm");
                    break;
                case "CL-file's":
                    Zoek("Algemeen CL locatie", "*.CL");
                    Zoek("Ontwikkel algemeen CL locatie", "*.CL");
                    Zoek("Historie algemeen CL locatie", "*.CL");
                    Zoek("Conv 21 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 21 CL locatie", "*.CL");
                    Zoek("Historie conv 21 CL locatie", "*.CL");
                    Zoek("Conv 22 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 22 CL locatie", "*.CL");
                    Zoek("Historie conv 22 CL locatie", "*.CL");
                    Zoek("Conv 23 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 23 CL locatie", "*.CL");
                    Zoek("Historie conv 23 CL locatie", "*.CL");
                    break;
                case "Zoek in Alle cl,eb,pnt":
                    Zoek("Ontwikkel algemeen CL locatie", "*.EB");
                    Zoek("Algemeen CL locatie", "*.CL");
                    Zoek("Ontwikkel algemeen CL locatie", "*.CL");
                    Zoek("Historie algemeen CL locatie", "*.CL");
                    Zoek("Conv 21 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 21 CL locatie", "*.CL");
                    Zoek("Historie conv 21 CL locatie", "*.CL");
                    Zoek("Conv 22 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 22 CL locatie", "*.CL");
                    Zoek("Historie conv 22 CL locatie", "*.CL");
                    Zoek("Conv 23 CL locatie", "*.CL");
                    Zoek("Ontwikkel conv 23 CL locatie", "*.CL");
                    Zoek("Historie conv 23 CL locatie", "*.CL");
                    Zoek("Scada punten locatie", "*.pnt");
                    Zoek("Experion C300 locatie", "*.*");
                    Zoek("Display locatie", "*.htm");
                    break;
                case "":
                    break;
                default:
                    MessageBox.Show("keuze nog niet in programma");
                    break;
            }

            if (gekozen != "")
            {
                Close();
                if (!IsCancelled)
                {
                    DataCL._MainForm.FileNaamStatusStrip.Text = "Zoeken klaar";
                    DataCL._MainForm.Gevonden.gevonden_text = ZoekStringTextBox.Text;
                    DataCL._MainForm.Gevonden.Text = "Gevonden variable " + ZoekStringTextBox.Text + " in volgende directory's";

                    if (!DataCL._MainForm.Gevonden.Visible)
                    {
                        DataCL._MainForm.Gevonden.Show();
                    }

                    DataCL._MainForm.Gevonden.WindowState = FormWindowState.Normal;
                }
                else
                {
                    DataCL._MainForm.Gevonden.WindowState = FormWindowState.Minimized;
                    DataCL._MainForm.FileNaamStatusStrip.Text = "Cancel Zoeken";
                }
            }
            LockWindowUpdate(IntPtr.Zero);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            ZoekStringTextBox.Text = textBoxHuidigeSel.Text.ToUpper();
        }

        private void SuperZoek(string StartDir, string ZoekText, string FileSoort, bool Recursive)
        {
            ZoekText = ZoekText.ToUpper();

            if (FileSoort == "*.EB")
            {
                // start dit bij eb is niet zo belangrijk, gaan we zelf kiezen.
                KeuzeZoekEbDir KeuzeEBDir = new KeuzeZoekEbDir();
                KeuzeEBDir.ShowDialog();
                InfoLabel.Text = "op zoek naar alle eb file's in deze directory's";
                for (int a = 0; a < KeuzeEBDir.listViewKeuzeDir.Items.Count; a++)
                {
                    if (KeuzeEBDir.listViewKeuzeDir.Items[a].Selected)
                    {
                        // haal alle file namen
                        StartDir = KeuzeEBDir.listViewKeuzeDir.Items[a].Text;
                        AddFileNamesToList(StartDir, FileSoort, Recursive);

                        InfoLabel.Text = "op zoek naar zoek string in deze file's";
                        Refresh();
                        // zoek in file namen
                        foreach (string fileName in fileEntries)
                        {
                            if (ZoekInFile(fileName, ZoekText) && !IsCancelled)
                            {
                                string[] row = { fileName /*, ZoekText*/ };
                                ListViewItem listItem = new ListViewItem(row);
                                DataCL._MainForm.Gevonden.listViewGevonden.Items.Add(listItem);
                            }
                        }

                        // xml van c300 file welke hieronder staan ook meenemen
                        AddFileNamesToList(StartDir, "*.XML", true);

                        InfoLabel.Text = "op zoek naar zoek string in deze file's";
                        Refresh();
                        // zoek in file namen
                        foreach (string fileName in fileEntries)
                        {
                            if (ZoekInFile(fileName, ZoekText) && !IsCancelled)
                            {
                                string[] row = { fileName /*, ZoekText*/ };
                                ListViewItem listItem = new ListViewItem(row);
                                DataCL._MainForm.Gevonden.listViewGevonden.Items.Add(listItem);
                            }
                        }

                    }

                }
            }
            else
            {
                InfoLabel.Text = "Zoek in " + StartDir;
                Refresh();
                DataCL._MainForm.FileNaamStatusStrip.Text = "Zoek " + ZoekText + " in " + StartDir;

                // haal alle file namen

                AddFileNamesToList(StartDir, FileSoort, Recursive);


                // zoek in file namen
                if (fileEntries != null)
                {
                    foreach (string fileName in fileEntries)
                    {
                        if (!IsCancelled && ZoekInFile(fileName, ZoekText))
                        {
                            string[] row = { fileName/*, ZoekText*/ };
                            ListViewItem listItem = new ListViewItem(row);
                            DataCL._MainForm.Gevonden.listViewGevonden.Items.Add(listItem);
                        }
                    }
                }

            } // end else cl of eb
            Close();
            DataCL._MainForm.Gevonden.PrieviewScherm.Clear();
        }

        private void AddFileNamesToList(string sourceDir, string FileSoort, bool Recursive)
        {
            CancelBut.Refresh();
            Application.DoEvents();

            Task t = Task.Run(() =>
            {
                if (!IsCancelled)
                {
                    try
                    {
                        if (Recursive)
                        {
                            fileEntries = Directory.GetFiles(sourceDir, FileSoort, SearchOption.AllDirectories);
                        }
                        else
                        {
                            fileEntries = Directory.GetFiles(sourceDir, FileSoort, SearchOption.TopDirectoryOnly);
                        };
                    }
                    catch
                    {
                        MessageBox.Show($"{sourceDir} bestaat niet.");
                    }
                   
                }
            });
            if (!IsCancelled)
            {
                t.Wait();
            }
        }


        private bool ZoekRecursion(string locatie)
        {
            bool Recursion = false;
            if (DataCL.AlgIniFile.Read(locatie + " Recursion") == "True")
            {
                Recursion = true;
            }
            return Recursion;
        }

        // haal data uit ini file's en roep dan superzoek aan.
        private void Zoek(string locatie, string ext)
        {

            if (!IsCancelled)
            {
                string zoek_path = DataCL.AlgIniFile.Read(locatie);
                bool Recursion = ZoekRecursion(locatie);
                InfoLabel.Text = string.Format("op zoek in : {0}", locatie);
                Refresh();
                SuperZoek(zoek_path, ZoekStringTextBox.Text, ext, Recursion);
            }
        }

        private void CancelBut_Click(object sender, EventArgs e)
        {
            IsCancelled = true;
            MessageBox.Show("Zoek onderbroken");
        }

        private bool ZoekInFile(string filenaam, string zoektekst)
        {
            bool ret = false;
            if (!IsCancelled)
            {
                InfoLabel.Text = "Zoek " + zoektekst + " in " + filenaam;
                Refresh();
                CancelBut.Refresh();
                Application.DoEvents();

                //RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
                //Regex regex = new Regex(zoektekst,options);


                //Task t = Task.Run(() =>
                //{
                    //using (StreamReader reader = new StreamReader(filenaam))
                    //{
                    //    string line;
                    //    while ((line = reader.ReadLine()) != null)
                    //    {
                    //        // Try to match each line against the Regex.
                    //        Match match = regex.Match(line);
                    //        if (match.Success)
                    //        {
                    //            ret = true;
                    //            break;
                    //        }
                    //    }
                    //}

                    using (FileStream fileStream = File.OpenRead(filenaam))
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {

                        string line;
                        while ((line = streamReader.ReadLine()) != null && !IsCancelled)
                        {
                            if (line.IndexOf(zoektekst, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                ret = true;
                                break;
                            }
                        }
                    }
                //});
                //if (!IsCancelled)
                //{
                //    t.Wait();
                //}
            }
            return ret;
        }

        // set standaard selectie op cl's
        private void ZoekNaarTags_Shown(object sender, EventArgs e)
        {
            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];

        }

        private void buttonHelpZoek_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hier kunt u zoeken naar tag/text.\n" +
                "U kunt regular expressions gebruiken\n" +
                "https://en.wikipedia.org/wiki/Regular_expression \n" +
                "Bv zoek naar 2.60PC02\n" +
                @"Maar ook uitgebreid bv (?<=\.) {2,}(?=[A-Z]" +
                "\nAt least two spaces are matched, but only if they occur directly\n" +
                "after a period(.) and before an uppercase letter.");
        }
    }
}

