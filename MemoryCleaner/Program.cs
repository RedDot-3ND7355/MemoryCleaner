using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MemoryCleaner
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static Mutex? _mutex;

        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, e) => ShowFatal(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    ShowFatal(ex);
            };

            const string mutexName = @"Local\MemoryCleaner_SingleInstance";
            _mutex = new Mutex(true, mutexName, out bool createdNew);

            if (!createdNew)
            {
                // Already running
                MessageBox.Show("Memory Cleaner is already running.", "Memory Cleaner");
                return;
            }

            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new NewDesign());
            }
            catch (Exception ex)
            {
                ShowFatal(ex);
            }
            finally
            {
                _mutex.ReleaseMutex();
                _mutex.Dispose();
            }
        }

        private static void ShowFatal(Exception ex)
        {
            try
            {
                string logPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "MemoryCleaner",
                    "crash.log");

                Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);
                File.WriteAllText(logPath, ex.ToString());

                MessageBox.Show(
                    ex.ToString(),
                    "MemoryCleaner crashed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch
            {
                // last resort
            }
        }
    }
}
