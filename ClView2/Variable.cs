using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClView2
{
    class Variable
    {
        private string FileNaam;
        private bool _zoek_variable = true;
        private String Regel_Temp;

        public Variable()
        {
            try
            {
                DataCL._MainForm.FileNaamStatusStrip.Text = "Zet Variable veld";
                FileNaam = Path.GetFileName(DataCL.FileNaam);

                // maak variable lijst en vul deze
                // vul meteen phase en steps grid

                _zoek_variable = true;
                //int regel_nummer;

                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(DataCL.FileNaam))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {

                        Regel_Temp = line.Trim();
                        string[] splitData;

                        if (Regel_Temp.Length < 3)
                            continue;

                        if (Regel_Temp[0] == Regel_Temp[1]) // bv --
                            continue;

                        // snel en dirty check
                        //string snel = "LPSEHI";
                        //string a = Regel_Temp.Substring(0, 1);
                        //string tweedechar = Regel_Temp.Substring(1, 1);

                        //int pos = snel.IndexOf(a, 0, StringComparison.Ordinal);
                        //if (pos < 0)
                        //    continue;

                        splitData = Regel_Temp.Split(new string[] { " ", ":", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);

                        //
                        //     if (_woord[0] == "PHASE")         alleen bij deze keywoorden verder plukken
                        //     if (_woord[0] == "SUBROUTINE")
                        //     if (_woord[0] == "STEP")
                        //     if (_woord[0] == "EXTERNAL")
                        //     if (_woord[0] == "LOCAL")
                        //     if (_woord[0] == "HOLD")
                        //     if (_woord[0] == "SHUTDOWN")
                        //

                        if (_zoek_variable)  // als eerste tekst phase gezien is, wordt dit false (er zijn daarna geen variable meer)
                        {

                            splitData[0] = splitData[0].ToUpper();
                            if (splitData[0] == "LOCAL" || splitData[0] == "EXTERNAL")
                            {
                                DataCL._TagEnBeschrijving.Add(splitData[1]);
                                DataCL._TagEnBeschrijving.Add(Regel_Temp);
                                continue;
                            }
                        }


                        // label ?
                        if (Char.IsLetter(Regel_Temp[0]))
                        {
                            int pos = Regel_Temp.IndexOf(":");
                            if (pos > 0 && pos < 8)
                            {
                                DataCL._MainForm.listBoxPhase.Items.Add(Regel_Temp);
                                continue;
                            }
                        }


                        if (splitData[0] == "PHASE" || splitData[0] == "STEP" || splitData[0] == "SUBROUTINE" || splitData[0] == "SHUTDOWN" || splitData[0] == "HOLD")
                        {
                            _zoek_variable = false; // eerste phase geen declararte van variable meer
                            DataCL._MainForm.listBoxPhase.Items.Add(Regel_Temp);
                            continue;
                        };
                        //return;// ZoekVariableReturn.NotFound;
                    }
                }
            
            }catch{}
        }
    }
}