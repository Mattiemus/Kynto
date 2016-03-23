using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace KyntoServerTester
{
    /// <summary>
    /// The main entrypoint of the tester application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // Create a thread for the kynto server.
            Thread ServerThread = new Thread(new ThreadStart(KyntoServerThread));
            ServerThread.Start();

            // Create the main forms.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Executes the kynto server.
        /// </summary>
        public static void KyntoServerThread()
        {
            // Create and run the kynto server.
            KyntoServer.Program.BaseDirectory = @"C:\Users\Matthew\Desktop\Toyusha\Kynto\bin\";
            KyntoServer.Program.Main(new string[] { });
        }
    }
}
