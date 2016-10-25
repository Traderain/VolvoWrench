using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.DG
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.GetCommandLineArgs().Any(x => Path.GetExtension(x) == ".dem" || Path.GetExtension(x) == ".sav"))
                if (Environment.GetCommandLineArgs().Any(x => Path.GetExtension(x) == ".dem"))
                    Application.Run(new Main(Environment.GetCommandLineArgs().First(x => Path.GetExtension(x) == ".dem")));
                else if (Environment.GetCommandLineArgs().Any(x => Path.GetExtension(x) == ".sav"))
                    Application.Run(new saveanalyzerform(Environment.GetCommandLineArgs().First(x => Path.GetExtension(x) == ".sav")));
                else
                    Application.Run(new Main());
            else
                Application.Run(new Main());
            //args[0] is the application path.
            //args[1] will be the file path.
            //args[n] will be any other arguments passed in.
        }
    }
}