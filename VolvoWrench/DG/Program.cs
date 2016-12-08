using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VolvoWrench.SaveStuff;

namespace VolvoWrench.DG
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var argsappended = args.ToList();
            argsappended.Add(AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData[0]); //This fixes the OpenWith on clickonce apps
            var cla = argsappended;            
            if (cla.Any(x => Path.GetExtension(x) == ".dem" || Path.GetExtension(x) == ".sav"))
                if (cla.Any(x => Path.GetExtension(x) == ".dem"))
                    Application.Run(new Main(cla.First(x => Path.GetExtension(x) == ".dem")));
                else if (cla.Any(x => Path.GetExtension(x) == ".sav"))
                    Application.Run(new saveanalyzerform(cla.First(x => Path.GetExtension(x) == ".sav")));
                else
                    Application.Run(new Main());
            else
                Application.Run(new Main());

        }
    }
}