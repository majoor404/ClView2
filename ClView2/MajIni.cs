using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ClView2
{

    class MajIni
    {
        public string Path;
        public List<string> list;
        private List<string> templist = new List<string>();
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        public MajIni(string IniPath)
        {
            // ?? als inipath is null dan exe+".ini"
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
            // kijk of deze bestaat, en lees in.
            try
            {
                string[] logFile = File.ReadAllLines(Path);
                list = new List<string>(logFile);
            }
            catch
            {
                list = new List<string>();
            }
        }

        ~MajIni()
        {
            SaveIniToFile();
        }

        public void SaveIniToFile()
        {
            //// save list data bij afsluiten.
            try
            {
                File.WriteAllLines(Path, list.ToArray());
            }
            catch { } // gewone gebruikers kunnen opslag paden niet opslaan.
        }

        public string Read(string Key, string Section = null)
        {
            int start = 0;
            if (Section != null)
            {
                // op zoek naar juiste sectie
                string section = "[" + Section + "]";
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == section)
                    {
                        start = i;
                        break;
                    }
                }
            }

            for (int i = start; i < list.Count; i++)
            {
                string regel = list[i];
                if (regel.Length > Key.Length)
                    regel = regel.Substring(0, Key.Length);
                if (regel == Key)
                    return StripNaam(list[i]);
            }
            return "";
        }

        public void Write(string Key, string Value, string Section = null)
        {
            SectieToTempList(Section);

            // kijk of key bestaat in lijst (verander of toevoegen)
            int bestaat = 0;
            for (int i = 0; i < templist.Count; i++)
            {
                string regel = templist[i];
                if (regel.Length > Key.Length)
                    regel = regel.Substring(0, Key.Length);
                if (regel == Key)
                {
                    bestaat = i;
                    break;
                }
            }
            if (bestaat > 0)
            {
                // verander
                templist[bestaat] = CombiNaam(Key, Value);
            }
            else
            {
                // toevoegen
                templist.Add(CombiNaam(Key, Value));
            }

            // en maak list weer compleet.
            list.AddRange(templist);
        }


        public void DeleteKey(string Key, string Section = null)
        {
            SectieToTempList(Section);

            // kijk of key bestaat in lijst
            int bestaat = 0;
            for (int i = 0; i < templist.Count; i++)
            {
                string regel = templist[i];
                if (regel.Length > Key.Length)
                    regel = regel.Substring(0, Key.Length);
                if (regel == Key)
                {
                    bestaat = i;
                    break;
                }
            }
            if (bestaat > 0)
                templist.RemoveAt(bestaat);

            // en maak list weer compleet.
            list.AddRange(templist);
        }

        public void DeleteSection(string Section = null)
        {
            SectieToTempList(Section);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        private string StripNaam(string v)
        {
            // return alles na de =
            // bv return 2_N05S12.CL bij v = "tabnaam=2_N05S12.CL"
            string regel = "";
            int pos = v.IndexOf('=');
            if (pos > 0)
                regel = v.Substring(pos + 1);
            return regel;
        }
        private string CombiNaam(string key, string value)
        {
            return key + "=" + value;
        }

        void SectieToTempList(string Section)
        {
            int start = -1;
            int eind = list.Count;
            templist.Clear();
            string section_start = "[" + Section + "]";
            // als er een sectie is dan haal dat stukje uit list
            // ga die aanpassen, en plaatst weer terug.
            if (Section != null)
            {

                // op zoek naar juiste sectie
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == section_start)
                    {
                        start = i;
                        break;
                    }
                }
                if (start > -1)
                {
                    // dan op zoek naar volgende sectie (of eind)
                    for (int i = start + 1; i < list.Count; i++)
                    {
                        if (list[i].Length > 0)
                        {
                            string a = list[i].Substring(0, 1);
                            if (a == "[")
                            {
                                eind = i;
                                break;
                            }
                        }
                    }
                    if (start == -1)
                    {
                        start = 0;
                    }
                    for (int i = start; i < eind; i++)
                    {
                        templist.Add(list[i]);
                    }
                    // delete regels in huidige lijst
                    list.RemoveRange(start, eind - start);
                }
            }
            else
            {
                // nieuwe section
                templist.Add(section_start);

            }
        }
    }
}