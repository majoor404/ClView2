using System;
using System.Collections.Generic;

namespace ClView2
{
    public class TabInfo
    {
        public string _filenaam;
        public string _tabnaam;
        public int _nummer;
        public int _huidigcursorpositie;
        public int toprow;
        public List<int> BookMark;
        public bool _editmode;           // worden niet opgeslagen in ini, alleen in runtime
        public bool _changefiledooredit; // worden niet opgeslagen in ini, alleen in runtime
        public bool _filegesavedintempdir; // worden niet opgeslagen in ini, alleen in runtime

        public TabInfo(int num)
        {
            this._nummer = num;
            string sectie = _nummer.ToString();
            _tabnaam = "";
            _filenaam = "";
            _huidigcursorpositie = 0;
            toprow = 0;
            BookMark = new List<int>();
            _editmode = false;
            _changefiledooredit = false;
            _filegesavedintempdir = false;
            try
            {
                _tabnaam = DataCL.TabsIniFile.Read("tabnaam", sectie);
                _filenaam = DataCL.TabsIniFile.Read("filenaam", sectie);
                _huidigcursorpositie = Convert.ToInt32(DataCL.TabsIniFile.Read("regelnr", sectie));

                int bookmarkAantal = Convert.ToInt32(DataCL.TabsIniFile.Read("bookmarkcount", sectie));

                for (int i = 0; i < bookmarkAantal + 1; i++)
                {
                    string naam = String.Format("BookMark{0}", i);
                    int bookmark = Convert.ToInt32(DataCL.TabsIniFile.Read(naam, sectie));
                    BookMark.Add(bookmark);
                }

                toprow = Convert.ToInt32(DataCL.TabsIniFile.Read("toprow", sectie));
            }
            catch (Exception)
            {
            }

        }

        ~TabInfo()
        {
            SaveInIniFile();
        }

        public void SaveInIniFile()
        {
            string sectie = _nummer.ToString();
            if (_tabnaam == "")
            {
                DataCL.TabsIniFile.DeleteSection(sectie);
            }
            else
            {
                DataCL.TabsIniFile.Write("tabnaam", _tabnaam, sectie);
                DataCL.TabsIniFile.Write("filenaam", _filenaam, sectie);
                DataCL.TabsIniFile.Write("regelnr", _huidigcursorpositie.ToString(), sectie);
                DataCL.TabsIniFile.Write("toprow", toprow.ToString(), sectie);

                DataCL.TabsIniFile.Write("bookmarkcount", BookMark.Count.ToString(), sectie);
                // als aantal 0, dan opruimen in ini file
                bool bool_opruim = DataCL.TabsIniFile.KeyExists("BookMark0", sectie);
                if (BookMark.Count == 0 && bool_opruim)
                {
                    for (int a = 0; a < 25; a++)
                    {
                        string naam = String.Format("BookMark{0}", a);
                        DataCL.TabsIniFile.DeleteKey(naam, sectie);
                    }
                }

                for (int i = 0; i < BookMark.Count; i++)
                {
                    string naam = String.Format("BookMark{0}", i);
                    DataCL.TabsIniFile.Write(naam, BookMark[i].ToString(), sectie);
                }
            }
        }
    }
}
