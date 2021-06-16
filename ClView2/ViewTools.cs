using System.Drawing;
using System.Windows.Forms;

namespace ClView2
{
    public class ViewTools
    {
        public ViewTools()
        {
        }

        public int GetLineNum()
        {
            int index = DataCL._MainForm.View.SelectionStart;
            return DataCL._MainForm.View.GetLineFromCharIndex(index) + 1;
        }

        public int TotaalAantalRegels()
        {
            return DataCL._MainForm.View.GetLineFromCharIndex(DataCL._MainForm.View.Text.Length) + 1;
        }

        // bovenste lijn welk in view nu te zien is.
        public int BovensteLijn()
        {
            return DataCL._MainForm.View.GetLineFromCharIndex(DataCL._MainForm.View.GetCharIndexFromPosition(new Point(1, 1)));
        }


        public void ZetSelectieInMidden()
        {
            DataCL._MainForm.FileNaamStatusStrip.Text = "Zet selectie in midden";
            int gevraagd_top = GetLineNum() - 15;
            if (gevraagd_top < 0)
                gevraagd_top = 0;
            ScrollToLine(gevraagd_top);
            DataCL._MainForm.FileNaamStatusStrip.Text = "Gereed";
        }


        public void ScrollToLine(int line)
        {
            // save the current selection to be restored later
            var selection = new { DataCL._MainForm.View.SelectionStart, DataCL._MainForm.View.SelectionLength };

            // select that line and scroll it to
            DataCL._MainForm.View.Select(DataCL._MainForm.View.GetFirstCharIndexFromLine(line) + 1, 0);
            DataCL._MainForm.View.ScrollToCaret();

            // restore selection
            DataCL._MainForm.View.Select(selection.SelectionStart, selection.SelectionLength);
        }

        // zet geseltie op woord nivo, neem _ mee.
        public void ZetSelectieMooi(RichTextBox richTextBox)
        {
                // bewaar selectie
                int start = richTextBox.SelectionStart;
                int lengte = richTextBox.SelectionLength;
                //string SelString = DataCL._view.SelectedText;

                if (lengte > 0)
                {
                    // pas begin en eind aan.
                    // eerst begin
                    char a = richTextBox.Text[start];
                    while (a == ' ')
                    {
                        start++;
                        //DataCL._view.SelectionStart++;
                        a = richTextBox.Text[start];
                    }
                    // nu eind
                    a = richTextBox.Text[start + lengte - 1];
                    while (a == ' ' || a == '\n')
                    {
                        lengte--;
                        a = richTextBox.Text[start + lengte - 1];
                    }
                    // als begin een '_' dan uitbreiden naar voren
                    if(start>1)
                        a = richTextBox.Text[start - 1];
                    if (a == '_')
                    {
                        start--;
                        lengte++;
                        a = richTextBox.Text[start - 1];
                        while (a != ' ')
                        {
                            start--;
                            lengte++;
                            a = richTextBox.Text[start - 1];
                        }
                    }
                    // als eind een '_' dan uitbreiden naar achter
                    if (start + lengte < richTextBox.Text.Length)
                        a = richTextBox.Text[start + lengte];
                    if (a == '_')
                    {
                        lengte++;
                        a = richTextBox.Text[start + lengte];
                        while (a != ' ' && a != '\n' && a != ')' && a != ';')
                        {
                            lengte++;
                            a = richTextBox.Text[start + lengte];
                        }
                    }
                    if (lengte > 0)
                    {
                        richTextBox.SelectionLength = lengte;
                        richTextBox.SelectionStart = start;
                    }
                }
                //DataCL.VrijgaveZetSelectieMooi = true;
        }

        public void ZetHint()
        {
            // zet maar 1 maal.
            if (DataCL._MainForm.View.SelectedText.Length > 0)
            {
                // haal uit tag alle data, eerst zoeken
                
                for (int a = 0; a < DataCL._TagEnBeschrijving.Count; a= a+2)
                {
                    if (DataCL._MainForm.View.SelectedText == DataCL._TagEnBeschrijving[a])
                    {
                        DataCL._MainForm.textBoxHint.Text = DataCL._TagEnBeschrijving[a+1];
                        
                        //DataCL._MainForm.toolStripComboZoek.Text = DataCL._MainForm.View.SelectedText;
                        break;
                    }
                }
                if (DataCL._MainForm.View.SelectedText.Length < 20)
                {
                    DataCL._MainForm.toolStripComboZoek.Text = DataCL._MainForm.View.SelectedText;
                    DataCL._MainForm.toolStripComboZoek_TextChanged(this, null);
                }
            }
        }
    }
}
