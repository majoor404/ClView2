using ClView2.Properties;
using printhelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ClView2
{
    public partial class MainForm : Form
    {
        private IniFile Alg_inifile;
        private IniFile Tabs_IniFile;
        private Tabs _Tabs;
        private Bullet _Bullet;
        public bool ZoekFromTop = false;

        private string opslagfile = Path.GetTempPath() + "ClView2File.ini";
        public Gevonden Gevonden;

        private bool Vrijgave_Opslag_Windows = false; // bool om window positie en size op te slaan
        private bool ZoekItemsOpslag = true; // om items in zoek dropdown op te slaan

        private readonly ViewTools ViewTool = new ViewTools();
        private readonly PrintTools PrintTool = new PrintTools();

        private readonly List<string> TagEnBeschrijving = new List<string>();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern bool LockWindowUpdate(IntPtr hWndLock);

        public MainForm()
        {
            InitializeComponent();
            //MessageBox.Show("1");
            String[] arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
            {
                //MessageBox.Show(arguments[0]);
                string pathnaarfile = Path.GetDirectoryName(arguments[0]);
                //MessageBox.Show(pathnaarfile);
                Alg_inifile = new IniFile(pathnaarfile + "\\ClView2.ini");
            }
            else
            {
                //MessageBox.Show("ini prog plaats");
                Alg_inifile = new IniFile("ClView2.ini"); // zelfde dir als programma
            }

            // bewaar algemene data

            DataCL._MainForm = this;
            //MessageBox.Show("2");
            Gevonden = new Gevonden();
            DataCL._TagEnBeschrijving = TagEnBeschrijving;
            DataCL._ViewTools = ViewTool;
            DataCL.AlgIniFile = Alg_inifile;

            // check of "Prive opslag locatie gebruiker" is ingevuld.
            // zo ja, copy vanuit deze remote ? locatie de TabsClView2.ini naar temp dir
            // bij einde programma zet deze file weer op remote locatie            
            string priveopslagtabsTemp = Path.GetTempPath() + "TabsClView2.ini";
            try
            {
                string priveopslagtabs = DataCL.AlgIniFile.Read("Prive opslag locatie gebruiker");
                if (File.Exists(priveopslagtabs + "\\TabsClView2.ini"))
                {
                    // copy
                    File.Copy(priveopslagtabs + "\\TabsClView2.ini", priveopslagtabsTemp, true);
                }
            }
            catch { }
            Tabs_IniFile = new IniFile(priveopslagtabsTemp);

            DataCL.TabsIniFile = Tabs_IniFile;

            DataCL._TagsForm = new TagsView
            {
                Visible = false
            };

            _Tabs = new Tabs(this)
            {
                _HuidigTab = 0
            };
            _Bullet = new Bullet(_Tabs);

            ColorLink();

            // probeer juiste font
            try
            {
                toolStripComboBoxFontGroteKeuze.Text = Settings.Default.Font_Grote.ToString();
                toolStripComboBoxFontKeuze.Text = Settings.Default.Font;
            }
            catch (Exception)
            {
            }
            //MessageBox.Show("3");

            if (arguments.Length > 1)
            {
                //MessageBox.Show(arguments.Length.ToString());
                //MessageBox.Show(arguments[1]);
                NieuweFileLaden(arguments[1]);
            }

            timer.Enabled = true;
        }

        #region UI

        /// <summary>
        ///  zet window form op juiste plek op scherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //int X = Convert.ToInt32(Settings.Default.Location_window_X);
            //int Y = Convert.ToInt32(Settings.Default.Location_window_Y);

            //string dum = string.Format("{0},{1}", X.ToString(), Y.ToString());
            //MessageBox.Show(dum);

            //    if (X < 0) X = 0;
            //    if (Y < 0) Y = 0;
            //    Point Point1 = new Point(X, Y);

            //    X = Convert.ToInt32(Settings.Default.Size_window_Width);
            //    Y = Convert.ToInt32(Settings.Default.Size_window_Height);
            //    Size Size1 = new Size(X, Y);
            //    dum = string.Format("{0},{1}", X.ToString(), Y.ToString());
            //    MessageBox.Show(dum);


            //WindowState = FormWindowState.Normal;
            //Location = Point1;
            //Size = Size1;
            Vrijgave_Opslag_Windows = true;
            ZetViewEnHintGoed();
        }

        private void setupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Instellingen _inst = new Instellingen();
            _inst.Show();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region UTILS

        /// <summary>
        /// bewaar locatie scherm in ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_LocationChanged(object sender, EventArgs e)

        {
            if (Vrijgave_Opslag_Windows)
            {
                //Settings.Default.Location_window_X = DataCL._MainForm.Location.X;
                //Settings.Default.Location_window_Y = DataCL._MainForm.Location.Y;
                //string dum = string.Format("{0},{1}", DataCL._MainForm.Location.X.ToString(), DataCL._MainForm.Location.Y.ToString());
                //MessageBox.Show("opslag " + dum);
            }
        }

        /// <summary>
        /// bewaar size window op scherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (Vrijgave_Opslag_Windows && (WindowState != FormWindowState.Minimized))
            {
                //string X_ = Size.Width.ToString();
                //string Y_ = Size.Height.ToString();
                //Settings.Default.Size_window_Width = Size.Width;
                //Settings.Default.Size_window_Height = Size.Height;
                //string dum = string.Format("{0},{1}", Size.Width.ToString(), Size.Height.ToString());
                //MessageBox.Show("opslag size" + dum);
            }
            ZetViewEnHintGoed();
        }

        /// <summary>
        /// plaatst en laad zien de tag window 
        /// </summary>
        private void regel_tag_view()
        {
            int X = Settings.Default.Location_tag_X;
            int Y = Settings.Default.Location_tag_Y;
            Point Point1 = new Point(X, Y);
            DataCL._TagsForm.Zet_Locatie(Point1);
            DataCL._TagsForm.Show();
        }

        /// <summary>
        /// Open File Keuze dialoog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFile_Click(object sender, EventArgs e)
        {
            if (TestOfLaatsteFileVeranderdIs())
            {
                DirectoryKeuze keuze = new DirectoryKeuze();
                if (keuze.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show(Alg_inifile.Path);
                    if (DataCL.Temp == DataCL.AlgIniFile.Read("Eb locatie"))
                    {
                        // eb file
                        FileNaamStatusStrip.Text = "laden";
                        openFileDialog.InitialDirectory = DataCL.Temp;
                        openFileDialog.Filter = "EB files (*.EB)|*.EB";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.FileName = "";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            DataCL._EB = new EBForm();
                            DataCL._EB.NieuweEBFile(openFileDialog.FileName);
                            DataCL._EB.ShowDialog();
                        }
                        openFileDialog.Dispose();   // meteen opruimen uit geheugen
                        DataCL._MainForm.Focus();
                        DataCL._MainForm.FileNaamStatusStrip.Text = "Gereed";
                    }
                    else
                    {
                        FileNaamStatusStrip.Text = "laden";
                        openFileDialog.InitialDirectory = DataCL.Temp;
                        openFileDialog.Filter = "CL files (*.CL)|*.CL";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.FileName = "";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            NieuweFileLaden(openFileDialog.FileName);
                        }
                        openFileDialog.Dispose();   // meteen opruimen uit geheugen
                        DataCL._MainForm.Focus();
                        DataCL._MainForm.FileNaamStatusStrip.Text = "Gereed";
                    }
                }
            }
        }

        private void View_SelectionChanged(object sender, EventArgs e)
        {
            // weg gehaald, bij zoeken en selecteren komt die in een loop

            //View_KeyPress(this, null); // regel nummer

            //if (View.SelectionLength > 1 && View.SelectionLength < 11)
            //{
            //    toolStripComboZoek.Text = View.SelectedText;
            //}
        }

        #endregion


        // file laden
        // aanroep zet in kleur
        // haal phase en subroetine's
        // haal variable
        public void NieuweFileLaden(string filenaam)
        {
            if (filenaam != null)
            {
                Cursor = Cursors.WaitCursor;

                /// hier gaat het fout, met top row en current pos wordt 1 ipv opgeslagen positie
                /// dus bewaar en zet aan eind terug

                int current_cursor_pos = 1;
                int current_toprow = 1;
                try
                {
                    current_cursor_pos = _Tabs.GetCurrentCursorPositie();
                    current_toprow = _Tabs.GetTopRow();
                }
                catch (Exception)
                {
                }

                //als huidige file veranderd, save deze
                if ((_Tabs._HuidigTab > 0) && _Tabs.TabBladen[_Tabs._HuidigTab - 1]._changefiledooredit)
                {
                    SaveChangeFileInTempDir();
                }

                DataCL.FileNaam = filenaam;

                _Tabs.UpdateTabNaamEnLocatie(filenaam);

                ColorLink();

                FileNaamStatusStrip.Text = "Laden van file " + filenaam;
                ZetViewKleur zet = new ZetViewKleur();        // zet in kleur

                DataCL._MainForm.listBoxPhase.Items.Clear();
                TagEnBeschrijving.Clear();

                Variable Var = new Variable();      // zet varable en phase veld.
                                                    //als tag window visible, even opniew laden met nieuwe data

                if (tagViewWindowToolStripMenuItem.Checked)
                {
                    DataCL._TagsForm.load_data_tagview();
                }
                PlaatsFormulierNaam(filenaam);

                toolStripComboBoxFontKeuze_TextChanged(this, null);
                toolStripComboBoxFontGroteKeuze_TextChanged(this, null);

                ZetEditModeItems(_Tabs.GetEditMode());

                Cursor = Cursors.Default;

                _Tabs.SetCurrentCursorPositie(current_cursor_pos);

                _Tabs.SetTopRow(current_toprow);

                FileNaamStatusStrip.Text = "Klaar Laden van file";
            }
        }

        private static void PlaatsFormulierNaam(string filenaam)
        {
            DataCL._MainForm.Text = Path.GetFileName(filenaam) + "   ---   ";

            for (int a = 0; a < DataCL._TagEnBeschrijving.Count; a = a + 2)
            {
                if (DataCL._TagEnBeschrijving[a] == "VERSIE")
                {
                    DataCL._MainForm.Text += DataCL._TagEnBeschrijving[a + 1];
                    break;
                }
            }
        }

        public void ZoekEnSelText(object sender, EventArgs e)
        {
            if (View.Text.Length != 0)
            {
                DataCL._MainForm.FileNaamStatusStrip.Text = "Zoek tekst";
                // als je met de hand zoek hebt ingevuld, dan ook even opslaan in lijst
                View.Focus();
                toolStripComboZoek_TextChanged(this, null);
                // zoek tekst welke staat in toolStripComboBox
                if (toolStripComboZoek.Text.Length > 0)
                {
                    int start_zoek = DataCL._MainForm.View.SelectionStart + 1;
                    if (ZoekFromTop) start_zoek = 0;

                    int pos = View.Find(toolStripComboZoek.Text, start_zoek + 1, RichTextBoxFinds.None);

                    ViewTool.ZetSelectieInMidden();
                    if (pos == -1)
                    {
                        MessageBox.Show("Volgende Niet Gevonden!,\nZoek Vorige met shift F3");
                    }
                    ZoekFromTop = false;
                }
            }
            else
            {
                _ = MessageBox.Show("Laad wel een file voordat u gaat zoeken in deze file.");
            }
        }

        public void ZoekEnSelTextReverse(object sender, EventArgs e)
        {
            DataCL._MainForm.FileNaamStatusStrip.Text = "Zoek tekst reverse";
            ZoekItemsOpslag = true;
            // zoek tekst welke staat in toolStripComboZoek
            RichTextBoxFinds Options = RichTextBoxFinds.None;
            Options |= RichTextBoxFinds.Reverse;
            if (toolStripComboZoek.Text.Length > 0)
            {
                // als geen selectie zelfde als zoek tekst, dan verder zoeken.
                if (DataCL._MainForm.View.SelectedText == toolStripComboZoek.Text)
                {
                    int zoekstart = DataCL._MainForm.View.SelectionStart - DataCL._MainForm.View.SelectedText.Length - 1;
                    int pos = View.Find(toolStripComboZoek.Text, 0, zoekstart, Options);
                    if (pos == -1)
                    {
                        MessageBox.Show("Vorige Niet Gevonden!,\nZoek Volgende met F3");
                    }
                }
                ViewTool.ZetSelectieInMidden();
            }
        }

        public void toolStripComboZoek_TextChanged(object sender, EventArgs e)
        {
            bool AlInLijst = false;
            toolStripComboZoek.Text = toolStripComboZoek.Text.ToUpper();
            if (ZoekItemsOpslag)
            {
                // nieuwe tekst in dropdown, voeg toe aan dropdown
                // max 10 stuks
                if (toolStripComboZoek.Items.Count > 10)
                {
                    toolStripComboZoek.Items.RemoveAt(0);
                }

                for (int i = 0; i < toolStripComboZoek.Items.Count; i++)
                {
                    string value = toolStripComboZoek.Items[i].ToString();
                    if (value == toolStripComboZoek.Text)
                    {
                        AlInLijst = true;
                    }
                }

                if (!AlInLijst)
                {
                    toolStripComboZoek.Items.Add(toolStripComboZoek.Text);
                }
            }
        }

        private void toolStripComboZoek_Enter(object sender, EventArgs e)
        {
            // zet opslag even uit
            ZoekItemsOpslag = false;
        }

        private void toolStripComboZoek_Leave(object sender, EventArgs e)
        {
            ZoekItemsOpslag = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }

        // dynamisch positie windows onderdelen goed zetten.
        private void ZetViewEnHintGoed()
        {
            int XL = Convert.ToInt32(PanelView.Location.X + 10);
            int YL = Convert.ToInt32(PanelView.Location.Y + 10);
            Point PointView = new Point(XL, YL);
            View.Location = PointView;

            XL = Convert.ToInt32(PanelView.Size.Width - 400);
            YL = Convert.ToInt32(PanelView.Size.Height - 70);
            Size SizeView = new Size(XL, YL);
            View.Size = SizeView;

            XL = Convert.ToInt32(PanelView.Location.X + 10);
            YL = Convert.ToInt32(YL + 45);
            PointView = new Point(XL, YL);
            textBoxHint.Location = PointView;

            XL = Convert.ToInt32(PanelView.Size.Width - 20);
            YL = Convert.ToInt32(textBoxHint.Size.Height);
            SizeView = new Size(XL, YL);
            textBoxHint.Size = SizeView;

            XL = Convert.ToInt32(PanelView.Location.X + View.Size.Width + 20);
            YL = Convert.ToInt32(PanelView.Location.Y + 10);
            PointView = new Point(XL, YL);
            listBoxPhase.Location = PointView;

            XL = Convert.ToInt32(PanelView.Size.Width - View.Size.Width - 30);
            YL = Convert.ToInt32(PanelView.Size.Height - 70);
            SizeView = new Size(XL, YL);
            listBoxPhase.Size = SizeView;

            XL = Convert.ToInt32(PanelView.Location.X + View.Size.Width - 33);
            YL = Convert.ToInt32(PanelView.Location.Y + 12);
            PointView = new Point(XL, YL);
            CloseView.Location = PointView;
        }

        private void zoekNaarTag_Click(object sender, EventArgs e)
        {
            if (TestOfLaatsteFileVeranderdIs())
            {
                ZoekNaarTags ZT = new ZoekNaarTags();
                if (View.SelectedText != "")
                {
                    ZT.textBoxHuidigeSel.Visible = true;
                    ZT.buttonCopy.Visible = true;
                    ZT.labelHuidigeSelectie.Visible = true;
                    ZT.textBoxHuidigeSel.Text = View.SelectedText;
                }
                else
                {
                    ZT.textBoxHuidigeSel.Visible = false;
                    ZT.buttonCopy.Visible = false;
                    ZT.labelHuidigeSelectie.Visible = false;
                }
                ZT.ShowDialog();

                DataCL._MainForm.Gevonden.TopMost = true;
                DataCL._MainForm.Gevonden.TopMost = false;
                // 'Steal' the focus.
                DataCL._MainForm.Gevonden.Activate();
            }
        }

        // zet regel nummer in window
        // los ik nu op op tijd bassis
        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            //_Tabs.SaveHuidigeCursorPositie();

            //if (_Tabs._HuidigTab > 0)
            //{
            //    ZetStatusRegel();
            //}
        }

        public void tagViewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zet tag window aan of uit en vul deze
            if (tagViewWindowToolStripMenuItem.Checked)
            {
                DataCL._TagsForm.Visible = true;
                regel_tag_view();
            }
            else
            {
                DataCL._TagsForm.Visible = false;
            }
        }

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            if (View.SelectedText.Length > 0)
            {
                ViewTool.ZetSelectieMooi(DataCL._MainForm.View);
                ViewTool.ZetHint();
            }
            else
            {
                textBoxHint.Text = "";
            }
        }

        #region Bookmarks
        // vlag
        private void toolStripZetBullit_Click(object sender, EventArgs e)
        {
            _Bullet.SetRemoveBullit();
        }

        private void toolStripButtonFlagForwards_Click(object sender, EventArgs e)
        {
            _Bullet.ForwardsClick();
        }

        private void toolStripButtonFlagReverse_Click(object sender, EventArgs e)
        {
            _Bullet.BackwardsClick();
        }

        #endregion

        // bepaal FileIsChange in edit mode
        // control F zoek funtie aanroep
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!View.ReadOnly)
            {
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._filegesavedintempdir = false;
            }

            if (keyData == (Keys.Control | Keys.D1))
            {
                LaadFileViaIndexNummer(1);
            }
            if (keyData == (Keys.Control | Keys.D2))
            {
                LaadFileViaIndexNummer(2);
            }
            if (keyData == (Keys.Control | Keys.D3))
            {
                LaadFileViaIndexNummer(3);
            }
            if (keyData == (Keys.Control | Keys.D4))
            {
                LaadFileViaIndexNummer(4);
            }
            if (keyData == (Keys.Control | Keys.D5))
            {
                LaadFileViaIndexNummer(5);
            }
            if (keyData == (Keys.Control | Keys.D6))
            {
                LaadFileViaIndexNummer(6);
            }

            if ((keyData == (Keys.Control | Keys.F)) || (keyData == Keys.F3 && View.SelectionLength < 1))
            {
                // zoek form en plaats deze in menu balk
                OpZoekNaarForm OZNF = new OpZoekNaarForm();
                if (toolStripComboZoek.Text.Length > 0)
                {
                    OZNF.ZoekVulText(toolStripComboZoek.Text);
                }

                if (OZNF.ShowDialog() == DialogResult.OK)
                {
                    if (OZNF.textBoxZoek.Text.Length > 0)
                    {
                        toolStripComboZoek.Text = OZNF.textBoxZoek.Text.ToUpper();
                        ZoekEnSelText(this, null);
                        return true;
                    }
                }
            }

            if (!View.ReadOnly)
            {
                // file is veranderd, pas aan in TAB
                // als eerste keer veranderd, dan plaats ik locatie file aan naar temp file
                if (!_Tabs.TabBladen[_Tabs._HuidigTab - 1]._changefiledooredit)
                {
                    SaveChangeFileInTempDir();
                }

                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._changefiledooredit = true;
                _Tabs.PlaatsLinks();
                PlaatsFormulierNaam(_Tabs.TabBladen[_Tabs._HuidigTab - 1]._filenaam);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SaveChangeFileInTempDir()
        {
            if (!_Tabs.TabBladen[_Tabs._HuidigTab - 1]._filegesavedintempdir)
            {

                // save file in temp directory, en pas in _tabs locatie hiervoor aan.
                List<string> tekst = new List<string>(View.Lines);

                string filenaamtemp = string.Format(Path.GetTempPath() + "Temp{0}.CL", _Tabs._HuidigTab.ToString());

                using (TextWriter tw = new StreamWriter(filenaamtemp))
                {
                    foreach (string s in tekst)
                    {
                        tw.WriteLine(s);
                    }
                }
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._filenaam = filenaamtemp;
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._filegesavedintempdir = true;
            }
        }

        // save cl File.
        private void saveclFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveNaarForm SN = new SaveNaarForm();
            SN.ShowDialog();
            FileNaamStatusStrip.Text = "save";
            saveFileDialog1.InitialDirectory = SN.save_locatie; // DataCL.Temp;
            saveFileDialog1.Filter = "CL files (*.CL)|*.CL";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // kan file niet saven onder zelde naam, want hij is geopend in dit programma
                // dus eerst in ander instance laden en dan die save.
                List<string> tekst = new List<string>(View.Lines);

                using (TextWriter tw = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (string s in tekst)
                    {
                        tw.WriteLine(s);
                    }
                }
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._filenaam = saveFileDialog1.FileName;
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._tabnaam = Path.GetFileName(saveFileDialog1.FileName);
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._changefiledooredit = false;
                _Tabs.TabBladen[_Tabs._HuidigTab - 1]._filegesavedintempdir = false;
                _Tabs.PlaatsLinks();
            }
            saveFileDialog1.Dispose();   // meteen opruimen uit geheugen

            DataCL._MainForm.Focus();
            DataCL._MainForm.FileNaamStatusStrip.Text = "Gereed";
        }

        // expand sub roetine
        private void expandCall_Click(object sender, EventArgs e)
        {
            int start = View.SelectionStart;
            int lengte = View.SelectionLength;
            string tekst = View.SelectedText;
            try
            {

                LockWindowUpdate(View.Handle);
                // test of eind is ')', anders vraag voor select auto hele call
                if ((tekst[tekst.Length - 1] != ')') && (tekst[tekst.Length - 1] != ';'))
                {
                    //MessageBox.Show("ga nu eerst hele call selecteren.");
                    while (tekst[tekst.Length - 1] != ')')
                    {
                        View.SelectionLength++;
                        tekst = View.SelectedText;
                    }
                }
                View.Refresh();

                // test of sub begind met CALL , anders naar voren uitbreiden
                int testcall = tekst.IndexOf("CALL");
                if (testcall < 0)
                {
                    int maxzoek = 30;
                    while (testcall < 0 && maxzoek > 0)
                    {
                        maxzoek--;
                        View.SelectionStart--;
                        View.SelectionLength++;
                        tekst = View.SelectedText;
                        testcall = tekst.IndexOf("CALL");
                    }
                }
                //testcall = tekst.IndexOf("CALL");

                // kijk of selectie begind met call
                // bv CALL CVPOS(ISSLCVP, ISSLCVV, 2_AUTV05.PVFL, 2250XA73.PVFL, BED_HUIS, &OFF, READY_COUNTER, 0, 1, AUTO, CVFB1_TIJD);

                // als subroetine zonder parameters, melding en exit!
                // als \n voor ( komt
                bool afbreken = true;
                //int return_ = tekst.IndexOf("\n");
                int pos_haak_sluit = tekst.IndexOf(")");
                int pos_haak_open = tekst.IndexOf("(");
                if (pos_haak_open < pos_haak_sluit && pos_haak_open > 0 && pos_haak_open < 25)
                {
                    afbreken = false;
                }
                else
                {
                    MessageBox.Show("Deze call heeft geen parameters of vreemde opbouw,\nkan dus niet expanderen!");
                }

                if (!afbreken)
                {
                    // andere manier van uitpluizen tekst
                    List<string> aanroep = new List<string>();
                    List<string> subparameters = new List<string>();
                    StringBuilder builder = new StringBuilder();
                    Form ex = new ExpandCall();
                    aanroep.Add("CALL");
                    int i = 4;
                    while (tekst[i] == ' ')
                    {
                        i++;
                    }
                    // dan nu de naam
                    while (tekst[i] != '(')
                    {
                        builder.Append(tekst[i++]);
                    }
                    aanroep.Add(builder.ToString());
                    builder.Clear();
                    while (tekst[i] == ' ')
                    {
                        i++;
                    }

                    int aantal_haken_open = 0;
                    bool doorgaan = true;
                    while (doorgaan) // normaal is ) einde call, maar een variable kan ook bv DI(2260ZK42_IN).PRTFL zijn.
                    {
                        i++;
                        if (tekst[i] == '(')
                        {
                            aantal_haken_open++;
                            //builder.Append(tekst[i]);
                        }
                        if (tekst[i] == ')')
                        {
                            aantal_haken_open--;
                            if (aantal_haken_open < 0)
                            {
                                aanroep.Add(builder.ToString());
                                doorgaan = false;
                            }
                        }
                        if (tekst[i] == ',')
                        {
                            aanroep.Add(builder.ToString());
                            builder.Clear();
                            i++;
                        }
                        if (tekst[i] == '-' && tekst[i + 1] == '-')
                        {
                            while (tekst[i] != '\n')
                            {
                                i++;
                            }
                        }
                        if (tekst[i] != ' ' && tekst[i] != '&' && tekst[i] != '\n')
                        {
                            builder.Append(tekst[i]);
                        }
                    }


                    string[] splitData;
                    //splitData = tekst.Split(new string[] { " ", ":", "(", ")", "&", ";", ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    splitData = aanroep.ToArray();

                    if (splitData[0] != "CALL")
                    {
                        MessageBox.Show("Start selectie met CALL");
                    }
                    else
                    {
                        // oke begind met call
                        // op zoek naar subroetine met de juiste naam
                        // start met SUBROUTINE splitData[1] en eindigd op END splitData[1]
                        string start_tekst = "SUBROUTINE " + splitData[1];
                        int start_pos_sub = View.Find(start_tekst, 1, RichTextBoxFinds.MatchCase);
                        if (start_pos_sub > 0)
                        {
                            // start gevonden
                            // zoek eind
                            string stop_tekst = splitData[1];
                            stop_tekst = stop_tekst.Trim();
                            stop_tekst = "END " + stop_tekst;
                            int eind_pos_sub = View.Find(stop_tekst, start_pos_sub, RichTextBoxFinds.MatchCase);
                            if (eind_pos_sub > 0)
                            {
                                // begin en eind gevonden.
                                View.SelectionStart = start_pos_sub;
                                View.SelectionLength = eind_pos_sub - start_pos_sub + stop_tekst.Length;
                                tekst = View.SelectedText;

                                Clipboard.Clear();
                                Clipboard.SetData(DataFormats.Rtf, View.SelectedRtf);


                                //View.Copy();
                                View.SelectionStart = start;
                                View.SelectionLength = lengte;
                                //ViewTool.ZetSelectieInMidden();

                                if (Clipboard.ContainsText(TextDataFormat.Rtf))
                                {
                                    DataCL._ExpandView.SelectedRtf
                                        = Clipboard.GetData(DataFormats.Rtf).ToString();
                                }
                                else
                                {
                                    MessageBox.Show("gaat wat fout met copy / paste");
                                }

                                //DataCL._ExpandView.Paste();

                                // probleem : soms is een variable naam ook een variable type, zoals hieronder MODE

                                //SUBROUTINE CVPOS (POSITIE               : IN NUMBER;
                                //&                SNELHEID              : IN NUMBER;
                                //&                AUTO_I_V_T            : IN LOGICAL;
                                //&                ALARM1                : IN OUT  LOGICAL;
                                //&                MODE                  : IN NUMBER;
                                //&                EIND_ZC_MODE          : IN MODE;

                                //// los dit op door op elke regel welke begind met SUB of &,
                                //// alles na : weg te gooien.
                                //// meteen SUBROUTINE naam ( ,weggooien

                                builder.Clear();

                                subparameters.Add("SUBROUTINE");
                                i = 10;
                                while (tekst[i] == ' ')
                                {
                                    i++;
                                }
                                // dan nu de naam
                                while (tekst[i] != '(')
                                {
                                    builder.Append(tekst[i++]);
                                }

                                subparameters.Add(builder.ToString());
                                builder.Clear();
                                while (tekst[i] == ' ')
                                {
                                    i++;
                                }

                                doorgaan = true;
                                while (doorgaan)
                                {
                                    i++;
                                    if (tekst[i] == ')')
                                    {
                                        subparameters.Add(builder.ToString());
                                        doorgaan = false;
                                    }

                                    if (tekst[i] == ',')
                                    {
                                        subparameters.Add(builder.ToString());
                                        builder.Clear();
                                        i++;
                                    }
                                    if (tekst[i] == '-' && tekst[i + 1] == '-')
                                    {
                                        while (tekst[i] != '\n')
                                        {
                                            i++;
                                        }
                                    }
                                    if (tekst[i] == ':')
                                    {
                                        subparameters.Add(builder.ToString());
                                        builder.Clear();
                                        while (tekst[i] != '\n')
                                        {
                                            i++;
                                            if (tekst[i] == ')')
                                            {
                                                doorgaan = false;
                                            }
                                        }
                                    }
                                    if (tekst[i] != ' ' && tekst[i] != '&' && tekst[i] != '\n')
                                    {
                                        builder.Append(tekst[i]);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Niet gevonden : " + stop_tekst);
                            }
                        }


                        // nu zoek en vervang tekst (en kleur)

                        string[] splitDataNieuw;
                        splitDataNieuw = subparameters.ToArray();

                        //splitDataNieuw = aanpasstring.ToString().Split(new string[] { " ", ":", "&", ";", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        int orgineelwaarde = 1;
                        for (int a = 2; a < splitDataNieuw.Length; a++)
                        {
                            orgineelwaarde++;
                            string vervang = splitData[orgineelwaarde];
                            string oud = splitDataNieuw[a];

                            int zoekstart = 0;
                            int pos = DataCL._ExpandView.Find(oud, zoekstart, RichTextBoxFinds.MatchCase & RichTextBoxFinds.WholeWord);
                            while (pos > -1)
                            {
                                // als gevonden tekst begind of eindigd met _ , dan is dat geen gevonden woord.
                                // als gevonden tekst remark, ook niet.
                                if (CheckHeelWoord(DataCL._ExpandView) && CheckColor(DataCL._ExpandView))
                                {
                                    DataCL._ExpandView.SelectedText = vervang;
                                    DataCL._ExpandView.SelectionStart = pos;
                                    DataCL._ExpandView.SelectionLength = vervang.Length;
                                    //DataCL._ExpandView.SelectionBackColor = Color.Yellow;
                                    DataCL._ExpandView.SelectionColor = Color.LightCoral;
                                    DataCL._ExpandView.SelectionFont = new Font(DataCL._MainForm.View.Font, FontStyle.Underline);
                                }
                                pos += splitData[orgineelwaarde].Length;
                                pos = DataCL._ExpandView.Find(oud, pos, RichTextBoxFinds.MatchCase & RichTextBoxFinds.WholeWord);
                            }
                        }

                        DataCL._ExpandView.SelectionStart = 0;
                        DataCL._ExpandView.SelectionLength = 0;
                        ex.ShowDialog();
                    }
                }
            } // afbreken
            catch
            {
                MessageBox.Show("Helaas lukt expanderen niet");
                LockWindowUpdate(IntPtr.Zero);
            }
            View.SelectionStart = start;
            View.SelectionLength = lengte;
            LockWindowUpdate(IntPtr.Zero);

        }

        // zet edit mode aan en uit
        private void editMode_Click(object sender, EventArgs e)
        {
            if (_Tabs._HuidigTab < 1)
            {
                MessageBox.Show("Edit op leeg tabblad 0 is nog niet mogelijk");
            }
            else
            {
                _Tabs.SaveEditMode(!_Tabs.GetEditMode());
                ZetEditModeItems(_Tabs.GetEditMode());
            }
        }

        private void ZetEditModeItems(bool EditMode)
        {
            editMode.Checked = EditMode;
            editToolStripMenuItem1.Checked = EditMode;
            View.ReadOnly = !EditMode;
            cutMenu.Enabled = EditMode;
            pasteMenu.Enabled = EditMode;
            cutToolStripMenuItem.Enabled = EditMode;
            pasteToolStripMenuItem.Enabled = EditMode;
            DataCL._MainForm.FileNaamStatusStrip.Text = EditMode ? "Edit Mode" : " ";
            DataCL._MainForm.BackColor = EditMode ? Color.Turquoise : Color.White;
            DataCL._MainForm.Refresh();
        }

        // close programma via kruisje in window
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Tabs.SaveHuidigeCursorPositie();
            _Tabs.TabsSave();

            string priveopslagtabsTemp = Path.GetTempPath() + "TabsClView2.ini";
            try
            {
                string priveopslagtabs = DataCL.AlgIniFile.Read("Prive opslag locatie gebruiker");

                if (File.Exists(priveopslagtabsTemp))
                {
                    // copy
                    File.Copy(priveopslagtabsTemp, priveopslagtabs + "\\TabsClView2.ini", true);
                    //MessageBox.Show("save");
                }
            }
            catch { }

            // kijk of er een tab open staat met een ChangeFileDoorEdit
            // zo ja, reageer hierop

            for (int i = 0; i < 6; i++)
            {
                if (_Tabs.TabBladen[i]._changefiledooredit)
                {
                    MessageBox.Show("Er is nog een file niet gesaved na verandering,\ndeze gaat verloren bij afsluiten!\nMaak deze Tab File leeg of Save deze!");
                    e.Cancel = true;
                }
            }
            MainForm_LocationChanged(this, null);
            MainForm_SizeChanged(this, null);
            Settings.Default.Save();
        }

        private void copyMenu_Click(object sender, EventArgs e)
        {
            View.Copy();
        }

        private void cutMenu_Click(object sender, EventArgs e)
        {
            View.Cut();
        }

        private void pasteMenu_Click(object sender, EventArgs e)
        {
            View.Paste();
        }

        private void statmentToevoegen_Click(object sender, EventArgs e)
        {
            FileNaamStatusStrip.Text = "statements bedenken";

            StringBuilder _TB = new StringBuilder();
            const int BufferSize = 128;

            using (FileStream fileStream = File.OpenRead(DataCL.FileNaam))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {

                //string RegelText;
                string Woord = "";
                int statement_nr = 1;
                int eerste_statement = 1;
                bool print;
                string line;
                int woordnummer = 0;
                string[] splitData;
                print = false;
                string Vorige_Woord = "";
                bool opnieuw_tellen;

                while ((line = streamReader.ReadLine()) != null)
                {
                    //Regel_Temp = line.Trim();
                    print = false;
                    woordnummer = 0;
                    bool doorgaan = true;

                    if (line.Length < 2)
                    {
                        //TB.AppendText(Environment.NewLine);
                        _TB.Append(Environment.NewLine);
                        continue;
                    }


                    if (line[0] == '-')
                    { // bv --
                        if (line[1] == '-')
                        {
                            //TB.AppendText("\t" + line);
                            //TB.AppendText(Environment.NewLine);
                            _TB.Append("\t" + line);
                            _TB.Append(Environment.NewLine);
                            continue;
                        }
                    }

                    splitData = line.Split(new string[] { " ", ":", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                    int splitlang = splitData.Length;

                    while ((woordnummer < splitlang) && doorgaan)
                    {
                        Woord = splitData[woordnummer++];

                        if (Woord == "--")
                        {
                            break;
                        }

                        switch (Woord)
                        {
                            case "CALL":
                            case "IF":
                            case "INITIATE":
                            case "SET":
                            case "GOTO":
                            case "EXIT":
                            case "SEND":
                            case "WAIT":
                            case "ENB":
                                statement_nr++;
                                if (!print)
                                {
                                    eerste_statement = statement_nr;
                                }

                                print = true;
                                break;
                        }

                        opnieuw_tellen = false;

                        bool check_phase = Woord.Equals("PHASE", StringComparison.Ordinal);
                        bool vorig_goto = Vorige_Woord.Equals("GOTO", StringComparison.Ordinal);
                        bool vorig_resume = Vorige_Woord.Equals("RESUME", StringComparison.Ordinal);

                        if (check_phase && !vorig_goto && !vorig_resume)
                        {
                            opnieuw_tellen = true;
                        }


                        bool woord_subroetine = Woord.Equals("SUBROUTINE", StringComparison.Ordinal);

                        if (!opnieuw_tellen && woord_subroetine && !vorig_goto && !vorig_resume)
                        {
                            opnieuw_tellen = true;
                        }

                        bool woord_restart = Woord.Equals("RESTART", StringComparison.Ordinal);

                        if (!opnieuw_tellen && woord_restart && !vorig_goto && !vorig_resume)
                        {
                            opnieuw_tellen = true;
                        }

                        bool woord_step = Woord.Equals("STEP", StringComparison.Ordinal);

                        if (!opnieuw_tellen && woord_step && !vorig_goto && !vorig_resume)
                        {
                            opnieuw_tellen = true;
                        }

                        bool woord_handler = Woord.Equals("HANDLER", StringComparison.Ordinal);

                        if (!opnieuw_tellen && woord_handler && !vorig_goto && !vorig_resume)
                        {
                            opnieuw_tellen = true;
                        }

                        if (opnieuw_tellen)
                        {
                            statement_nr = 0;
                            doorgaan = false;
                        }
                        Vorige_Woord = Woord;
                    }

                    if (print)
                    {
                        line = eerste_statement.ToString() + "\t" + line;
                    }
                    else
                    {
                        line = "\t" + line;
                    }
                    _TB.Append(line);
                    _TB.Append(Environment.NewLine);

                }
                // save gemaakte file

                //StatementForm SF = new StatementForm();
                //string opslag = Path.GetTempPath() + "temp.ls";
                //File.WriteAllText(@opslag, _TB.ToString());
                //ZetViewKleur ZKS = new ZetViewKleur(@opslag, SF.richTextBox);
                //SF.ShowDialog();

                string nieuwe_file_naam = DataCL.FileNaam;
                nieuwe_file_naam = Path.GetFileName(nieuwe_file_naam);
                nieuwe_file_naam = Path.ChangeExtension(nieuwe_file_naam, ".LS");
                nieuwe_file_naam = Path.GetTempPath() + nieuwe_file_naam;
                File.WriteAllText(@nieuwe_file_naam, _TB.ToString());
                NieuweFileLaden(nieuwe_file_naam);

                FileNaamStatusStrip.Text = "Gereed";
            }
        }

        // is selectie een heel woord ?
        private bool CheckHeelWoord(RichTextBox tekst)
        {
            // als bij gevonden tag er een _ of er achter zit, dan niet vervangen.
            // of een ander char

            int bewaar_start = DataCL._ExpandView.SelectionStart;
            int bewaar_lengte = DataCL._ExpandView.SelectionLength;

            tekst.SelectionLength = 1;
            tekst.SelectionStart = tekst.SelectionStart - 1;
            if (tekst.SelectedText == "." || tekst.SelectedText == "_" || char.IsLetter(tekst.SelectedText[0]))
            {
                DataCL._ExpandView.SelectionStart = bewaar_start;
                DataCL._ExpandView.SelectionLength = bewaar_lengte;
                return false;
            }

            tekst.SelectionStart = bewaar_start + bewaar_lengte;
            if (tekst.SelectedText == "." || tekst.SelectedText == "_" || char.IsLetter(tekst.SelectedText[0]))
            {
                DataCL._ExpandView.SelectionStart = bewaar_start;
                DataCL._ExpandView.SelectionLength = bewaar_lengte;
                return false;
            }

            DataCL._ExpandView.SelectionStart = bewaar_start;
            DataCL._ExpandView.SelectionLength = bewaar_lengte;
            return true;
        }

        // is selectie in remark tekst ?
        private bool CheckColor(RichTextBox tekst)
        {
            if (tekst.SelectionColor == Color.Blue)
            {
                return false;
            }

            return true;
        }

        // font type aanpassen
        private void toolStripComboBoxFontKeuze_TextChanged(object sender, EventArgs e)
        {
            float groot = int.Parse(toolStripComboBoxFontGroteKeuze.Text);
            DataCL._MainForm.View.SelectAll();
            DataCL._MainForm.View.SelectionFont = new Font(toolStripComboBoxFontKeuze.Text, groot, FontStyle.Regular);
            DataCL._MainForm.View.SelectionStart = 0;
            DataCL._MainForm.View.SelectionLength = 0;
            DataCL._MainForm.listBoxPhase.Font = DataCL._MainForm.View.SelectionFont;

            Settings.Default.Font = toolStripComboBoxFontKeuze.Text;

        }

        // font grote aanpassen
        private void toolStripComboBoxFontGroteKeuze_TextChanged(object sender, EventArgs e)
        {
            // als er een selectie is, bewaar deze en zet later terug
            int startselectie = 0;
            int lengteselectie = 0;
            bool selbewaar = false;
            if (View.SelectedText.Length > 0)
            {
                startselectie = View.SelectionStart;
                lengteselectie = View.SelectionLength;
                selbewaar = true;
            }

            DataCL._MainForm.View.SelectAll();
            float groot = int.Parse(toolStripComboBoxFontGroteKeuze.Text);
            DataCL._MainForm.View.SelectionFont = new Font(toolStripComboBoxFontKeuze.Text, groot, FontStyle.Regular);
            DataCL._MainForm.View.SelectionStart = 0;
            DataCL._MainForm.View.SelectionLength = 0;
            DataCL._MainForm.listBoxPhase.Font = DataCL._MainForm.View.SelectionFont;
            Settings.Default.Font_Grote = System.Convert.ToInt32(toolStripComboBoxFontGroteKeuze.Text);

            if (selbewaar)
            {
                View.SelectionStart = startselectie;
                View.SelectionLength = lengteselectie;
            }
        }

        // print gebeuren
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bewaar = toolStripComboBoxFontGroteKeuze.Text;

            if (toolStripComboBoxFontGroteKeuze.Text != "10")
            {
                toolStripComboBoxFontGroteKeuze.Text = "10";
            }

            System.Drawing.Printing.PageSettings PSS = new System.Drawing.Printing.PageSettings();
            PSS.Margins.Left = (int)(1f * 100); // margins are in inches
            PSS.Margins.Right = (int)(1f * 100);
            PSS.Margins.Top = (int)(1.5f * 100);
            PSS.Margins.Bottom = (int)(1.5f * 100);
            PSS.Landscape = false;
            Exception ex = new Exception("Exception Occurred While Printing");
            string header = "CLView 2.0 door R.Majoor    Geprint op: " + System.DateTime.Today.ToLongDateString() + "  " + System.DateTime.Now.ToShortTimeString();
            Font f = new Font("Arial", 8, FontStyle.Regular); // Font for the header
            PrintTool.GeneralPrintForm("CL View", View.Rtf, ref ex, PSS, true, header, f, true); // Overload 3 with page and header settings

            toolStripComboBoxFontGroteKeuze.Text = bewaar;
        }

        // geklikt op fase
        private void listBoxPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataCL._MainForm.View.Find(listBoxPhase.GetItemText(listBoxPhase.SelectedItem));
            DataCL._ViewTools.ZetSelectieInMidden();
            DataCL._MainForm.Focus();
        }

        // click op 1 van de 6 oude cl file's in menu balk
        private void CL1_Click(object sender, EventArgs e)
        {
            //als huidige file veranderd, save deze
            if ((_Tabs._HuidigTab > 0) && _Tabs.TabBladen[_Tabs._HuidigTab - 1]._changefiledooredit)
            {
                SaveChangeFileInTempDir();
            }

            // welke link geklikt ?
            ToolStripLabel toolStripLabel1 = (ToolStripLabel)sender;
            int index = Convert.ToInt32(toolStripLabel1.Tag.ToString());

            // test of file welke bij link hoord nog wel bestaat (buiten programma om verwijderd)
            // als dat zo is, verwijderd link/tab
            if (TestOfFileBestaatBijDezeLink(index))
            {
                LaadFileViaIndexNummer(index);
            }
        }

        /// <summary>
        /// als file niet meer bestaat bij link/tab, verwijders deze dan
        /// </summary>
        /// <param name="index">gekozen tab blad</param>
        /// <returns>false als niet openen</returns>
        private bool TestOfFileBestaatBijDezeLink(int index)
        {
            string filenaam = _Tabs.TabBladen[index - 1]._filenaam;

            if (File.Exists(filenaam))
            {
                return true;
            }
            else
            {
                // verwijder tab
                _Tabs._HuidigTab = index; // anders gaat het fout bij CloseView_Click
                CloseView_Click(this, null);
                return false;
            }
        }

        public void LaadFileViaIndexNummer(int index)
        {
            LockWindowUpdate(View.Handle);

            // save huidige positie document
            _Tabs.SaveHuidigeCursorPositie();

            string filenaam = _Tabs.TabBladen[index - 1]._filenaam;

            _Tabs._HuidigTab = index;

            if (File.Exists(filenaam))
            {
                NieuweFileLaden(filenaam);

                // zet top row terug
                int charindex = -1;
                int top_row = _Tabs.GetTopRow();
                int cursorpositie = _Tabs.GetCurrentCursorPositie();

                // door bullits plaatsen gaat toprow aan de haal, dus save voor setBullits
                _Bullet.SetBullit();

                if (top_row > 1)
                {
                    charindex = View.GetFirstCharIndexFromLine(Convert.ToInt32(top_row) - 1);
                }

                if (charindex > -1)
                {
                    View.Select(charindex, 1);
                    View.ScrollToCaret();
                }

                // zet cursor positie
                View.Select(cursorpositie, 1);

                View.SelectionLength = 0;
                LockWindowUpdate(IntPtr.Zero);
            }
            else
            {
                ColorLink();
            }
        }

        // print selectie
        private void printSelectieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bewaar = toolStripComboBoxFontGroteKeuze.Text;

            if (toolStripComboBoxFontGroteKeuze.Text != "10")
            {
                toolStripComboBoxFontGroteKeuze.Text = "10";
            }

            RichTextBox tempRB = new RichTextBox();
            View.Copy();
            tempRB.Paste();

            System.Drawing.Printing.PageSettings PSS = new System.Drawing.Printing.PageSettings();
            PSS.Margins.Left = (int)(1f * 100); // margins are in inches
            PSS.Margins.Right = (int)(1f * 100);
            PSS.Margins.Top = (int)(1.5f * 100);
            PSS.Margins.Bottom = (int)(1.5f * 100);
            PSS.Landscape = false;
            Exception ex = new Exception("Exception Occurred While Printing");
            string header = "CLView 2.0 door R.Majoor    Geprint op: " + System.DateTime.Today.ToLongDateString() + "  " + System.DateTime.Now.ToShortTimeString();
            Font f = new Font("Arial", 8, FontStyle.Regular); // Font for the header
            PrintTool.GeneralPrintForm("CL View", tempRB.Rtf, ref ex, PSS, true, header, f, true); // Overload 3 with page and header settings
            toolStripComboBoxFontGroteKeuze.Text = bewaar;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Visible)
            {
                toolStripComboBoxFontKeuze_TextChanged(this, null);
            }
        }


        // close view button visible/invisible
        private void View_TextChanged(object sender, EventArgs e)
        {
            CloseView.Visible = ViewTool.TotaalAantalRegels() > 0 ? true : false; ;
        }

        /// <summary>
        ///  sluit huidige tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseView_Click(object sender, EventArgs e)
        {
            _Tabs.Close();
            _Tabs._HuidigTab = 0;
            ColorLink();
            ZetEditModeItems(false);
            View.Text = "";
            DataCL._MainForm.listBoxPhase.Items.Clear();
            TagEnBeschrijving.Clear();
            CloseView.Visible = false;
            Text = ""; // caption form
        }


        /// <summary>
        /// verander afhankelijk van text de juiste link van kleur 
        /// </summary>
        private void ColorLink()
        {
            CL1.LinkColor = Color.LightCoral;
            CL2.LinkColor = Color.LightCoral;
            CL3.LinkColor = Color.LightCoral;
            CL4.LinkColor = Color.LightCoral;
            CL5.LinkColor = Color.LightCoral;
            CL6.LinkColor = Color.LightCoral;

            int index = _Tabs._HuidigTab;
            switch (index)
            {
                case 1:
                    CL1.LinkColor = Color.Blue;
                    break;
                case 2:
                    CL2.LinkColor = Color.Blue;
                    break;
                case 3:
                    CL3.LinkColor = Color.Blue;
                    break;
                case 4:
                    CL4.LinkColor = Color.Blue;
                    break;
                case 5:
                    CL5.LinkColor = Color.Blue;
                    break;
                case 6:
                    CL6.LinkColor = Color.Blue;
                    break;
            }
        }
        public bool TestOfLaatsteFileVeranderdIs()
        {
            if (!_Tabs.TabBladen[5]._changefiledooredit)
            {
                // ja je mag nieuwe file openen
                return true;
            }
            else
            {
                // niet nieuwe file laden
                MessageBox.Show("Je kan geen nieuwe file openen, eerst laatste tab saven of verwijderen!");
                return false;
            }
        }

        // check of er een file gevraagd is door ander instance
        // tevens toolstrip update regel en gegeven.
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (File.Exists(opslagfile))
            {
                timer.Enabled = false;
                string file;
                using (StreamReader sr = new StreamReader(opslagfile))
                {
                    file = sr.ReadLine();
                }
                File.Delete(opslagfile);
                NieuweFileLaden(file);
                //MessageBox.Show(Alg_inifile.Path);
                timer.Enabled = true;
            }

            _Tabs.SaveHuidigeCursorPositie();
            if (_Tabs._HuidigTab > 0)
            {
                toolStripStatusRegelInfo.Text = " " + _Tabs.GetTopRow()
                                            + " / " + ViewTool.GetLineNum().ToString()
                                            + " / " + ViewTool.TotaalAantalRegels() + " ";
            }
            else
            {
                toolStripStatusRegelInfo.Text = " 0 / 0 / 0 ";
            }
        }

        // geef file naam welke staat op index postie voor gevonden formulier
        public string GetFileNaamTab(int index)
        {
            string ret = "";
            ret = _Tabs.TabBladen[index]._filenaam;
            return ret;
        }

        private void GevondenSchermToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataCL._MainForm.Gevonden.TopMost = true;
            DataCL._MainForm.Gevonden.TopMost = false;
            DataCL._MainForm.Gevonden.WindowState = FormWindowState.Normal;
            // 'Steal' the focus.
            DataCL._MainForm.Gevonden.Activate();
        }

        private void zoekSelecteerdeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DataCL._MainForm.View.SelectedText.Length > 0 && DataCL._MainForm.View.SelectedText.Length < 20)
            {
                DataCL._MainForm.toolStripComboZoek.Text = DataCL._MainForm.View.SelectedText;
                ZoekEnSelText(this, null);
            }
        }

        private void toolStripButtonHighLightRegex_Click(object sender, EventArgs e)
        {
            KleurRegex KR = new KleurRegex();
            KR.ShowDialog();

            //string tokens = KR.textBoxKleurZoek.Text;
            if (KR.textBoxKleurZoek.Text != "")
            {
                Regex rex = new Regex(KR.textBoxKleurZoek.Text);
                MatchCollection mc = rex.Matches(DataCL._MainForm.View.Text);
                int StartCursorPosition = DataCL._MainForm.View.SelectionStart;
                foreach (Match m in mc)
                {
                    int startIndex = m.Index;
                    int StopIndex = m.Length;
                    DataCL._MainForm.View.Select(startIndex, StopIndex);
                    //DataCL._MainForm.View.SelectionColor = Color.Fuchsia;
                    DataCL._MainForm.View.SelectionBackColor = Color.Aqua;
                    DataCL._MainForm.View.SelectionStart = StartCursorPosition;


                    DataCL._MainForm.View.SelectionColor = Color.Black;
                    DataCL._MainForm.View.SelectionBackColor = Color.White;
                }
            }
        }
    }
}
