using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClView2
{
    class ZetViewKleur : IDisposable
    {
        private const String CR = "\r";
        private const String LF = "\n";
        private const String CRLF = "\r\n";
        private const String TERMINATOR = "}";
        private const String SLASH = "\\";
        private const String PARCODE = "\\par ";
        private const String KWCODE = "\\cf2\\fs16 ";
        private const String COMMENTCODE = "\\cf3\\fs16 ";
        private const String PLAINCODE = "\\plain\\fs16\\cf0 ";  // plain black for other text
                                                                 // use sizeof()-1 to skip terminating '\0' in stream writes for above

        private const String SPECIALRTFCHARS = "{}\\";

        private const String RTFHEADER = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang2057{\\fonttbl{\\f0\\fmodern\\fprq1\\fcharset0 Courier;}}\r\n";

        // orgineel
        //const String RTFCTABLE = "{\\colortbl\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red255\\green0\\blue255;\\red0\\green128\\blue0;}\r\n\\deflang2057\\pard\\plain\\f0\\fs16\\cf0 ";
        private const String RTFCTABLE = "{\\colortbl;\\red0\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}";

        private Color COMMENTCOL = Color.FromKnownColor(KnownColor.Blue);
        private Color PLAINCOL = Color.FromKnownColor(KnownColor.Black);
        private Color KWCOL = Color.FromKnownColor(KnownColor.Green);
        private Color EDITCOL = Color.FromKnownColor(KnownColor.Red);

        private const int NUMKEYWORDS = 52;

        // Keywords
        String[] Keywords =
                     { "LOCAL" , "LOGICAL" , "EXTERNAL" , "AT" , "NUMBER" , "ARRAY" ,
                "EXTERNAL" , "SEQUENCE" , "PHASE" , "CALL" , "IF" , "THEN" ,
                "SET" , "AND" , "ON" , "OFF" , "OR" , "GOTO" ,
                "RESTSTRT" , "NOT" , "INACTIVE" , "PROGRAM" , "MAN" , "AUTO" ,
                "OPERATOR" , "PROGRAM" , "ELSE" , "SEND" , "STEP" , "WAIT" ,
                "BLOCK" , "END" , "STRING" , "HOLD" , "HANDLER" , "WHEN" ,
                "RESUME" , "SHUTDOWN" , "SUBROUTINE" , "EXIT" , "PAUSE" , "IN" ,
                "OUT" , "MODATTR" , "MODE" , "RESET" , "INITIATE" , "MCMODE" ,
                "STATE" , "OAUTO" , "PMAN" , "TIME" };

        enum _status { rsSuccess, rsNoError, rsReadError };
        private _status _return = _status.rsNoError;
        private char c;
        private char[] _naarUTF16 = new char[2];
        private String token = "";
        private StreamReader _StreamIn;
        private MemoryStream _StreamOut;

        private void WriteRTFHeader()
        {
            try
            {
                schrijf_string(RTFHEADER);
                schrijf_string(RTFCTABLE);
            }
            catch
            {
                MessageBox.Show("Failed to write header and ctable to stream!");
            }
        }

        private void WriteRTFTerminator()
        {
            schrijf_string(TERMINATOR);
        }

        public ZetViewKleur()
        {
            try
            {
                DataCL._MainForm.FileNaamStatusStrip.Text = "Zet in Kleur";
                _StreamIn = new StreamReader(DataCL.FileNaam);
                _StreamOut = new MemoryStream();
                WriteRTFHeader();
                processStream();
                WriteRTFTerminator();
                _StreamOut.Seek(0, SeekOrigin.Begin);
                DataCL._MainForm.View.LoadFile(_StreamOut, RichTextBoxStreamType.RichText);
                _StreamIn.Close();
                _StreamOut.Close();
            }
            catch { };
        }

        public ZetViewKleur(string file, RichTextBox output)
        {
            _StreamIn = new StreamReader(file);
            _StreamOut = new MemoryStream();
            WriteRTFHeader();
            processStream();
            WriteRTFTerminator();
            _StreamOut.Seek(0, SeekOrigin.Begin);
            output.LoadFile(_StreamOut, RichTextBoxStreamType.RichText);
            _StreamIn.Close();
            _StreamOut.Close();
        }

        private void ReadWriteChar()
        {
            WriteChar();
            ReadChar();
        }

        void processStream()
        {

            while (_StreamIn.Peek() >= 0)
            {
                c = (char)_StreamIn.Read();
                try
                {
                    while (!_StreamIn.EndOfStream)
                    {
                        // read en pas write als het een token is!
                        ReadWriteToken();
                        if (_return != _status.rsSuccess)
                            // b.v. --dsgfghfds
                            ReadWriteLineComment();
                        if (_return != _status.rsSuccess)
                            ReadWriteChar();
                    }
                }
                catch
                {
                }
            }
        }

        void ReadWriteToken()
        {
            token = "";
            ReadToken();
            if (_return == _status.rsSuccess)
                WriteToken();
        }

        void ReadToken()
        {
            // haal geheel token ( haal 1 woord )

            _return = _status.rsNoError;

            token = "";
            try
            {
                while ((char.IsLetter(c) || c == '_') && (!_StreamIn.EndOfStream))
                {
                    token = token + c;
                    ReadChar();
                }
                if (!token.Equals("",StringComparison.Ordinal)) // != "")
                    _return = _status.rsSuccess;
            }
            catch
            {
                _return = _status.rsReadError;
            }

        }

        void ReadChar()
        {
            _return = _status.rsNoError;
            try
            {
                if (!_StreamIn.EndOfStream)
                    c = (char)_StreamIn.Read();
            }
            catch
            {
                _return = _status.rsReadError;
            }
        }

        void WriteToken()
        {
            _return = _status.rsNoError;

            if (KeyWord())
            {
                schrijf_string(KWCODE);
                schrijf_string(token);
                schrijf_string(PLAINCODE);
            }
            else
            {
                schrijf_string(token); 
            }
        }

        bool KeyWord()
        {
            bool isKW = false;
            int i = 0;
            while (i < NUMKEYWORDS && isKW == false)
            {
                if (token == Keywords[i])
                    isKW = true;
                else
                    ++i;
            }
            return isKW;
        }

        void ReadWriteLineComment()
        {
            _return = _status.rsNoError;
            char d = '\0';
            char e = '\0';
            // try block starts here
            if (c == '-')
            {
                d = c; // zie een -, kan ook bv -6 zijn. ipv -- remarks
                ReadChar();
                if (c != '-')
                {
                    e = c;
                    c = d;
                    WriteChar();
                    c = e;
                    //WriteChar();
                }
                else
                {
                    schrijf_string(COMMENTCODE);
                    schrijf_char(c);
                    schrijf_char(c);

                    while (!(c == '\r') && (!_StreamIn.EndOfStream))
                    {
                        ReadChar();
                        WriteChar();
                    }
                    if (!_StreamIn.EndOfStream)
                    {
                        schrijf_string(PLAINCODE);
                        ReadChar();
                    }
                }
                _return = _status.rsSuccess;
            }
        }

        void WriteChar()
        {
            HandleSpecialChars();
            schrijf_char(c);
        }

        void HandleSpecialChars()
        {

            if (SPECIALRTFCHARS.Contains(c))
                schrijf_string(SLASH);
            else
                HandleCR();
        }

        void HandleCR()
        {
            if (c == '\r')
            {
                schrijf_string(PARCODE);
            }
        }

        void schrijf_string(String code)
        {
            _StreamOut.Write(Encoding.ASCII.GetBytes(code), 0, code.Length);
        }

        void schrijf_char(char c)
        {
            _naarUTF16[0] = c;
            _StreamOut.Write(Encoding.ASCII.GetBytes(_naarUTF16), 0, 1);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                _StreamIn.Close();
                _StreamOut.Close();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}