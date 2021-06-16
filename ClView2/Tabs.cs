using System;
using System.Collections.Generic;
using System.IO;

namespace ClView2
{

    class Tabs
    {
        public TabInfo[] TabBladen = new TabInfo[6];
        private MainForm _mainForm;
        private int _huidigtab;
        

        public int GetTopRow()
        {
            if (_huidigtab > 0)
            {
                return TabBladen[_huidigtab - 1].toprow;
            }
            else
            {
                return 1;
            }
        }

        public void SetTopRow(int regel)
        {
            if (_huidigtab > 0)
                TabBladen[_huidigtab - 1].toprow = regel;
        }

        public int GetCurrentCursorPositie()
        {
            if (_huidigtab > 0)
            {
                return TabBladen[_huidigtab - 1]._huidigcursorpositie;
            }
            else
            {
                return 1;
            }
        }

        public void SetCurrentCursorPositie(int pos)
        {
            if(_huidigtab > 0)
                TabBladen[_huidigtab - 1]._huidigcursorpositie = pos;
        }

        public int _HuidigTab
        {
            get { return _huidigtab; }
            set
            {
                _huidigtab = value;
                _mainForm.TabNummer.Text = _huidigtab.ToString();
                _mainForm.TabNummer.Tag = _huidigtab;
            }
        }


        public Tabs(MainForm _mainForm)
        {
            this._mainForm = _mainForm;     // bewaar mainform pointer
            for (int i = 0; i < 6; i++)
            {
                TabBladen[i] = new TabInfo(i);
            }

            PlaatsLinks();
        }

        public void TabsSave()
        {
            for (int i = 0; i < 6; i++)
            {
                TabBladen[i].SaveInIniFile();
            }
        }

        public void SaveHuidigeCursorPositie()
        {
            if (_huidigtab > 0)
            {
                TabBladen[_huidigtab - 1]._huidigcursorpositie = _mainForm.View.SelectionStart;
                TabBladen[_huidigtab - 1].toprow = DataCL._ViewTools.BovensteLijn() + 1;
            }
        }

        // als nog niet in lijst, toevoegen
        // kom hier na laden nieuwe file
        public void UpdateTabNaamEnLocatie(string filenaam)
        {
            if (filenaam != TabBladen[0]._filenaam && filenaam != TabBladen[1]._filenaam &&
                filenaam != TabBladen[2]._filenaam && filenaam != TabBladen[3]._filenaam &&
                filenaam != TabBladen[4]._filenaam && filenaam != TabBladen[5]._filenaam)
            {
                SaveHuidigeCursorPositie();

                // nieuwe file, schuif alles 1 plek opzij
                Copy(5, 4);
                Copy(4, 3);
                Copy(3, 2);
                Copy(2, 1);
                Copy(1, 0);

                TabBladen[0]._filenaam = filenaam;
                TabBladen[0]._tabnaam = Path.GetFileName(filenaam);
                TabBladen[0]._huidigcursorpositie = 0;
                TabBladen[0]._nummer = 0;
                TabBladen[0].BookMark.Clear();
                TabBladen[0]._changefiledooredit = false;
                TabBladen[0]._editmode = false;
                TabBladen[0]._filegesavedintempdir = false;

                // dus nieuwe file op positie 1
                _HuidigTab = 1;
                PlaatsLinks();
            }
        }

        public void PlaatsLinks()
        {
            _mainForm.CL1.Text = TabBladen[0]._tabnaam;
            _mainForm.CL2.Text = TabBladen[1]._tabnaam;
            _mainForm.CL3.Text = TabBladen[2]._tabnaam;
            _mainForm.CL4.Text = TabBladen[3]._tabnaam;
            _mainForm.CL5.Text = TabBladen[4]._tabnaam;
            _mainForm.CL6.Text = TabBladen[5]._tabnaam;

            _mainForm.CL1.ToolTipText = TabBladen[0]._filenaam;
            _mainForm.CL2.ToolTipText = TabBladen[1]._filenaam;
            _mainForm.CL3.ToolTipText = TabBladen[2]._filenaam;
            _mainForm.CL4.ToolTipText = TabBladen[3]._filenaam;
            _mainForm.CL5.ToolTipText = TabBladen[4]._filenaam;
            _mainForm.CL6.ToolTipText = TabBladen[5]._filenaam;

            if (TabBladen[0]._changefiledooredit)
                _mainForm.CL1.Text = TabBladen[0]._tabnaam + "#";
            if (TabBladen[1]._changefiledooredit)
                _mainForm.CL2.Text = TabBladen[1]._tabnaam + "#";
            if (TabBladen[2]._changefiledooredit)
                _mainForm.CL3.Text = TabBladen[2]._tabnaam + "#";
            if (TabBladen[3]._changefiledooredit)
                _mainForm.CL4.Text = TabBladen[3]._tabnaam + "#";
            if (TabBladen[4]._changefiledooredit)
                _mainForm.CL5.Text = TabBladen[4]._tabnaam + "#";
            if (TabBladen[5]._changefiledooredit)
                _mainForm.CL6.Text = TabBladen[5]._tabnaam + "#";
        }

        public void SetBookMark(int regelnr)
        {
            if (_HuidigTab > 0)
                TabBladen[_HuidigTab - 1].BookMark.Add(regelnr);
            TabBladen[_HuidigTab - 1].SaveInIniFile();
        }

        public void RemoveBookMark(int regelnr)
        {
            if (_HuidigTab > 0)
                TabBladen[_HuidigTab - 1].BookMark.Remove(regelnr);
            TabBladen[_HuidigTab - 1].SaveInIniFile();
        }

        public bool BookMarkAanwezig()
        {
            return (BookMarkGetCount() > 0) ? true : false;
        }

        public int BookMarkGetCount()
        {
            if (_HuidigTab > 0)
                return TabBladen[_HuidigTab - 1].BookMark.Count;
            return 0;
        }

        public void BookMarkSort()
        {
            if (_HuidigTab > 0)
                TabBladen[_HuidigTab - 1].BookMark.Sort();
        }

        public int GetBookMark(int a)
        {
            return TabBladen[_HuidigTab - 1].BookMark[a];
        }

        public void BookMarkReverse()
        {
            if (_HuidigTab > 0)
                TabBladen[_HuidigTab - 1].BookMark.Reverse();
        }

        /// <summary>
        /// Sluit huidige tab blad
        /// </summary>
        public void Close()
        {
            switch (_HuidigTab)
            {
                // als 1 sluit, copy 
                case 1:
                    Copy(0, 1);
                    Copy(1, 2);
                    Copy(2, 3);
                    Copy(3, 4);
                    Copy(4, 5);
                    Clear(5);
                    break;
                case 2:
                    Copy(1, 2);
                    Copy(2, 3);
                    Copy(3, 4);
                    Copy(4, 5);
                    Clear(5);
                    break;
                case 3:
                    Copy(2, 3);
                    Copy(3, 4);
                    Copy(4, 5);
                    Clear(5);
                    break;
                case 4:
                    Copy(3, 4);
                    Copy(4, 5);
                    Clear(5);
                    break;
                case 5:
                    Copy(4, 5);
                    Clear(5);
                    break;
                case 6:
                    Clear(5);
                    break;
                default:
                case 0:
                    break;
            }
            PlaatsLinks();
        }

        /// <summary>
        /// copy gegevens van tablad B naar Tablad A
        /// </summary>
        /// <param name="A"> Doel </param>
        /// <param name="B"> OorSprong </param>
        private void Copy(int A, int B)
        {
            // als file copy, en file is temp, dan ook filenaam aanpassen
            // string filenaamtemp = String.Format(Path.GetTempPath() + "Temp{0}.CL", _Tabs._HuidigTab.ToString());
            if (TabBladen[B]._changefiledooredit)
            {
                string filenaamtemp = String.Format(Path.GetTempPath() + "Temp{0}.CL", A+1);
                File.Delete(filenaamtemp);
                File.Move(TabBladen[B]._filenaam, filenaamtemp);
                TabBladen[B]._filenaam = filenaamtemp;
            }

            TabBladen[A]._filenaam = TabBladen[B]._filenaam;
            TabBladen[A]._tabnaam = TabBladen[B]._tabnaam;
            TabBladen[A]._huidigcursorpositie = TabBladen[B]._huidigcursorpositie;
            TabBladen[A].toprow = TabBladen[B].toprow;
            TabBladen[A]._editmode = TabBladen[B]._editmode;
            TabBladen[A]._changefiledooredit = TabBladen[B]._changefiledooredit;
            TabBladen[A]._filegesavedintempdir = TabBladen[B]._filegesavedintempdir;
            TabBladen[A].BookMark = new List<int>(TabBladen[B].BookMark);
            TabBladen[A]._nummer = A;
        }

        private void Clear(int A)
        {
            TabBladen[A]._filenaam = "";
            TabBladen[A]._tabnaam = "";
            TabBladen[A]._huidigcursorpositie = 0;
            TabBladen[A].toprow = 0;
            TabBladen[A]._nummer = A;
            TabBladen[A]._editmode = false;
            TabBladen[A]._changefiledooredit = false;
            TabBladen[A]._filegesavedintempdir = false;
            TabBladen[A].BookMark.Clear();
        }

        public void SaveEditMode(bool edit)
        {
            if (_HuidigTab > 0)
                TabBladen[_HuidigTab - 1]._editmode = edit;
        }

        internal bool GetEditMode()
        {
            if (_HuidigTab > 0)
            {
                return TabBladen[_HuidigTab - 1]._editmode;
            }
            else
            {
                return false;
            }
            
        }
    }
}
