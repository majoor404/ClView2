using System.Collections.Generic;
using System.Windows.Forms;

namespace ClView2
{
    class DataCL
    {
        // data welk in elk formulier bekend moet zijn

        public static MainForm _MainForm { get; set; }
        public static TagsView _TagsForm { get; set; }
        //public static IniFile EigenIniFile { get; set; }
        public static IniFile AlgIniFile { get; set; }
        public static IniFile TabsIniFile { get; set; }
        public static string FileNaam { get; set; }
        public static string Temp { get; set; }
        public static ViewTools _ViewTools { get; set; }
        public static List<string> _TagEnBeschrijving { get; set; }
        public static RichTextBox _ExpandView { get; set; }
        public static EBForm _EB { get; set; }
    }
}
