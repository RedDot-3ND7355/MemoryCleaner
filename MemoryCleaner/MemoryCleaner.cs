using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MemoryCleaner
{
    public class _MemoryCleaner
    {
        RAMcs RAMcs = new RAMcs();
        private void wait(int milliseconds)
        {
            Timer timer_wait = new Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            timer_wait.Interval = milliseconds;
            timer_wait.Enabled = true;
            timer_wait.Start();
            timer_wait.Tick += (s, e) =>
            {
                timer_wait.Enabled = false;
                timer_wait.Stop();
            };
            while (timer_wait.Enabled)
            {
                Application.DoEvents();
            }
        }

        // Check if ran as admin
        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public void CleanMem(bool advanced = false, bool cached = true)
        {
            try
            {
                // Make process list with opened Applications (TESTING PURPOSES)
                // var processes = Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero).ToArray();

                // Make process list with all Applications
                var processes = Process.GetProcesses();
                Form1.CurrentForm.ResetLauncherLog();
                int toclean_count = processes.Count();
                if (processes.Count() > 0)
                {
                    Form1.CurrentForm.AddLauncherLog($"Cleaning [{toclean_count + " Process's"}] Memory...");
                    if (RAMcs.IsCounters)
                    {
                        var curr_ram = new RAMcs().Current_Usage();
                        var rounded_curr = Math.Round((Decimal)curr_ram, 2, MidpointRounding.AwayFromZero);
                        Form1.CurrentForm.AddLauncherLog("Before: " + rounded_curr + "%");
                    }
                    else
                    {
                        var curr_ram = new RAMcs().No_Counters_Curr_Usage();
                        var rounded_curr = Math.Round((Decimal)curr_ram, 2, MidpointRounding.AwayFromZero);
                        Form1.CurrentForm.AddLauncherLog("Before: " + rounded_curr + "%");
                    }
                    using (var Dispo = new DispoData())
                    {
                        foreach (Process prs_ss in processes)
                        {
                            if (advanced)
                                Form1.CurrentForm.AddLauncherLog("Cleaning: " + prs_ss.ProcessName);
                            if (prs_ss != null)
                                try
                                {
                                    prs_ss.MinWorkingSet = (IntPtr)(300000);
                                }
                                catch
                                {
                                    if (advanced)
                                        Form1.CurrentForm.AddLauncherLog("Ignored System Process: " + prs_ss.ProcessName);
                                    toclean_count--;
                                }
                        }
                        GCSettings.LatencyMode = GCLatencyMode.Interactive;
                        GC.Collect();
                        GC.Collect(1, GCCollectionMode.Forced, blocking: false);
                        GC.Collect(2, GCCollectionMode.Forced, blocking: false);
                        GC.Collect(3, GCCollectionMode.Forced, blocking: false);
                        GC.WaitForPendingFinalizers();
                        // Standby Cleaner
                        try
                        {
                            if (cached && IsAdministrator())
                            {
                                Form1.CurrentForm.AddLauncherLog("Clearing Standby Cache...");
                                new Win32_NtSetSystemInformation().ClearStandbyCache();
                                Form1.CurrentForm.AddLauncherLog("Cleared Standby Cache");
                            }
                            else if (cached && !IsAdministrator())
                            {
                                Form1.CurrentForm.AddLauncherLog("Clearing Standby Cache requires admin.");
                            }
                        } 
                        catch 
                        {
                            Form1.CurrentForm.AddLauncherLog("Failed to clear Standby Cache.");
                        }
                    }
                    wait(2500);
                    if (RAMcs.IsCounters)
                    {
                        var af_ram = new RAMcs().Current_Usage();
                        var rounded_af = Math.Round((Decimal)af_ram, 2, MidpointRounding.AwayFromZero);
                        Form1.CurrentForm.AddLauncherLog("After: " + rounded_af + "%");
                    }
                    else
                    {
                        var af_ram = new RAMcs().No_Counters_Curr_Usage();
                        var rounded_af = Math.Round((Decimal)af_ram, 2, MidpointRounding.AwayFromZero);
                        Form1.CurrentForm.AddLauncherLog("After: " + rounded_af + "%");
                    }
                    Form1.CurrentForm.AddLauncherLog($"Memory [{toclean_count + " Process's"}] Cleaned");
                }
            }
            catch
            {
                Form1.CurrentForm.AddLauncherLog("[Error] Failed Cleaning Memory!");
            }
        }

        // Dispose
        class DispoData : IDisposable
        {
            public void Dispose() { }
        }

    }
}
