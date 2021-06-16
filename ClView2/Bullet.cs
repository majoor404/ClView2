using System;
using System.Drawing;

namespace ClView2
{
    /// <summary>
    /// Regeld tekenen en bijhouden van vlag/bullets
    /// </summary>

    class Bullet
    {
        private Tabs _Tabs;
        private int _aantalBullets;

        private Color BulletTextColor = Color.Black;
        private Color BulletBackColor = Color.Yellow;

        private Color NormaalTextColor = Color.Black;
        private Color NormaalBackColor = Color.White;

        private int _AantalBookMarks
        {
            get { return _aantalBullets; }
            set
            {
                _aantalBullets = value;
                DataCL._MainForm.AantalBookmarks.Text = _aantalBullets.ToString();
            }
        }


        public Bullet(Tabs tabs)
        {
            _Tabs = tabs;
        }

        private int GetLineNum()
        {
            return DataCL._MainForm.View.GetLineFromCharIndex(DataCL._MainForm.View.SelectionStart) + 1;
        }

        internal void SetRemoveBullit()
        {
            // zet op deze regel een picture ? en bewaar regel in vlagtabel
            Point huidige_pos;
            int index = DataCL._MainForm.View.SelectionStart;
            huidige_pos = DataCL._MainForm.View.GetPositionFromCharIndex(index);
            huidige_pos.X = 1;
            DataCL._MainForm.View.SelectionStart = DataCL._MainForm.View.GetCharIndexFromPosition(huidige_pos);
            DataCL._MainForm.View.SelectionLength = Math.Max(1, DataCL._MainForm.View.Lines[DataCL._MainForm.View.GetLineFromCharIndex(DataCL._MainForm.View.SelectionStart)].Length);


            if (DataCL._MainForm.View.SelectionBackColor == BulletBackColor)
            {
                // remove flag
                DataCL._MainForm.View.SelectionColor = NormaalTextColor;
                DataCL._MainForm.View.SelectionBackColor = NormaalBackColor;
                _Tabs.RemoveBookMark(GetLineNum() - 1);
            }
            else
            {
                // plaats flag
                DataCL._MainForm.View.SelectionColor = BulletTextColor;
                DataCL._MainForm.View.SelectionBackColor = BulletBackColor;
                _Tabs.SetBookMark(GetLineNum() - 1);
            }
            DataCL._MainForm.View.SelectionLength = 0;
            _AantalBookMarks = _Tabs.BookMarkGetCount();

        }

        internal void ForwardsClick()
        {
            if (_Tabs.BookMarkAanwezig())
            {
                _Tabs.BookMarkSort();
                int huidige_regel = GetLineNum() - 1;
                for (int a = 0; a < _Tabs.BookMarkGetCount(); a++)
                {
                    int opgeslagen_regel = _Tabs.GetBookMark(a);

                    if (opgeslagen_regel > huidige_regel)
                    {
                        // ga naar juiste regel
                        if (opgeslagen_regel > 16)
                            DataCL._MainForm.View.Select(DataCL._MainForm.View.GetFirstCharIndexFromLine(opgeslagen_regel - 15) + 1, 0);
                        DataCL._MainForm.View.ScrollToCaret();
                        DataCL._MainForm.View.Select(DataCL._MainForm.View.GetFirstCharIndexFromLine(opgeslagen_regel) + 1, 0);
                        DataCL._MainForm.View.SelectionLength = 1;
                        break;
                    }

                }
            }
        }

        internal void BackwardsClick()
        {
            if (_Tabs.BookMarkAanwezig())
            {

                _Tabs.BookMarkSort();
                _Tabs.BookMarkReverse();

                int huidige_regel = DataCL._MainForm.View.GetLineFromCharIndex(DataCL._MainForm.View.SelectionStart) - 1;
                for (int a = 0; a < _Tabs.BookMarkGetCount(); a++)
                {
                    int opgeslagen_regel = _Tabs.GetBookMark(a);

                    if (opgeslagen_regel < huidige_regel)
                    {
                        // ga naar juiste regel
                        int ganaar = opgeslagen_regel - 15;
                        if (ganaar < 0)
                        {
                            ganaar = 0;
                        }
                        DataCL._MainForm.View.Select(DataCL._MainForm.View.GetFirstCharIndexFromLine(ganaar) + 1, 0);
                        DataCL._MainForm.View.ScrollToCaret();
                        DataCL._MainForm.View.Select(DataCL._MainForm.View.GetFirstCharIndexFromLine(opgeslagen_regel) + 1, 0);
                        DataCL._MainForm.View.SelectionLength = 1;
                        break;
                    }

                }

            }
        }

        // zet alle bullets (als file geladen wordt)
        public void SetBullit()
        {
            for (int i = 0; i < _Tabs.BookMarkGetCount(); i++)
            {
                var regel_vlag = _Tabs.GetBookMark(i);
                DataCL._MainForm.View.SelectionStart = DataCL._MainForm.View.GetFirstCharIndexFromLine(regel_vlag);
                DataCL._MainForm.View.SelectionLength = Math.Max(1, DataCL._MainForm.View.Lines[regel_vlag].Length);

                DataCL._MainForm.View.SelectionColor = BulletTextColor;
                DataCL._MainForm.View.SelectionBackColor = BulletBackColor;

            }
            _AantalBookMarks = _Tabs.BookMarkGetCount();
        }
    }
}
