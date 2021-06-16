using ClView2.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.VisualBasic.ApplicationServices;

//namespace ClView2
//    static class Program
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            //Application.Run(new MainForm());
//            string[] args = Environment.GetCommandLineArgs();
//            SingleInstanceController controller = new SingleInstanceController();
//            controller.Run(args);
//        }
//    }

//    public class SingleInstanceController : WindowsFormsApplicationBase
//    {
//        public SingleInstanceController()
//        {
//            IsSingleInstance = true;

//            StartupNextInstance += this_StartupNextInstance;
//        }

//        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
//        {
//            MainForm form = MainForm as MainForm; //My derived form type
//            form.NieuweFileLaden(e.CommandLine[1]);
//        }

//        protected override void OnCreateMainForm()
//        {
//            MainForm = new MainForm();
//        }
//    }
//}

namespace ClView2
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "CLView 2.0";
            bool createdNew;

            using (mutex = new Mutex(true, appName, out createdNew))
                if (!createdNew)
                {
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id != current.Id)
                        {
                            String[] arguments = Environment.GetCommandLineArgs();
                            if (arguments.Length > 1)
                            {
                                string opslagfile = Path.GetTempPath() + "ClView2File.ini";
                                using (StreamWriter writer = File.CreateText(opslagfile))
                                {
                                    writer.WriteLine(arguments[1]);
                                }
                            }
                            SetForegroundWindow(process.MainWindowHandle);
                            break;
                        }
                    }
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
        }
    }
}