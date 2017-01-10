using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CrashReporterDotNET;
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
            Application.ThreadException += ApplicationThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            var argsappended = args.ToList();
            argsappended.Add(AppDomain.CurrentDomain?.SetupInformation?.ActivationArguments?.ActivationData[0]); //This fixes the OpenWith on clickonce apps
            var cla = argsappended;
            if (cla.Any(x => Path.GetExtension(x) == ".dem" || Path.GetExtension(x) == ".sav"))
                if (cla.Any(x => Path.GetExtension(x) == ".dem"))
                    Application.Run(new Main(cla.First(x => Path.GetExtension(x) == ".dem")));
                else if (cla.Any(x => Path.GetExtension(x) == ".sav"))
                    Application.Run(new Saveanalyzerform(cla.First(x => Path.GetExtension(x) == ".sav")));
                else
                    Application.Run(new Main());
            else
                Application.Run(new Main());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            ReportCrash((Exception)unhandledExceptionEventArgs.ExceptionObject);
            Environment.Exit(0);
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e) => ReportCrash(e.Exception);

        private static void ReportCrash(Exception exception) => new ReportCrash { ToEmail = "hambalko.bence@gmail.com" }.Send(exception);
    }
}