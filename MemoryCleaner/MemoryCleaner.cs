using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCleaner
{
    public class _MemoryCleaner
    {
        private readonly RAMcs _ram = new();
        private readonly SemaphoreSlim _cleanLock = new(1, 1);

        [DllImport("psapi.dll")]
        private static extern bool EmptyWorkingSet(IntPtr hProcess);

        private static bool IsAdministrator()
        {
            using var identity = WindowsIdentity.GetCurrent();
            return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Trims process working sets and optionally purges the standby list.
        /// Safe to call from a background thread; UI logging is marshalled by Form1.
        /// </summary>
        public void CleanMem(bool advanced = false, bool cached = true)
        {
            // Ignore overlapping manual/timer cleans
            if (!_cleanLock.Wait(0))
            {
                NewDesign.CurrentForm?.AddLauncherLog("Clean already running — skipped.");
                return;
            }

            try
            {
                NewDesign.CurrentForm?.ResetLauncherLog();
                NewDesign.CurrentForm?.AddLauncherLog("Starting memory clean...");

                float beforePercent = _ram.GetUsagePercent();
                ulong beforeAvailable = _ram.GetAvailableBytes();

                NewDesign.CurrentForm?.AddLauncherLog(
                    $"Before: {beforePercent:F2}% used ({FormatBytes(beforeAvailable)} free)");

                Process[] processes = Process.GetProcesses();
                int cleaned = 0;
                int ignoredBlacklist = 0;
                int ignoredSystem = 0;

                foreach (Process process in processes)
                {
                    using (process)
                    {
                        string processFileName = process.ProcessName + ".exe";

                        if (BlacklistHandler.IsBlacklisted(processFileName))
                        {
                            ignoredBlacklist++;
                            if (advanced)
                                NewDesign.CurrentForm?.AddLauncherLog("Ignored blacklisted: " + process.ProcessName);
                            continue;
                        }

                        try
                        {
                            if (EmptyWorkingSet(process.Handle))
                            {
                                cleaned++;
                                if (advanced)
                                    NewDesign.CurrentForm?.AddLauncherLog("Cleaned: " + process.ProcessName);
                            }
                            else
                            {
                                ignoredSystem++;
                                if (advanced)
                                    NewDesign.CurrentForm?.AddLauncherLog("Skipped (access denied): " + process.ProcessName);
                            }
                        }
                        catch
                        {
                            // System/protected/exited processes
                            ignoredSystem++;
                            if (advanced)
                                NewDesign.CurrentForm?.AddLauncherLog("Ignored system process: " + process.ProcessName);
                        }
                    }
                }

                // Light GC for this app only — not a system RAM cleaner
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized, blocking: false);
                GC.WaitForPendingFinalizers();

                // Standby list purge (requires admin + SeProfileSingleProcessPrivilege)
                if (cached)
                {
                    if (IsAdministrator())
                    {
                        try
                        {
                            NewDesign.CurrentForm?.AddLauncherLog("Clearing standby cache...");
                            Win32_NtSetSystemInformation.ClearStandbyCache();
                            NewDesign.CurrentForm?.AddLauncherLog("Standby cache cleared.");
                        }
                        catch (Exception ex)
                        {
                            NewDesign.CurrentForm?.AddLauncherLog("Failed to clear standby cache: " + ex.Message);
                        }
                    }
                    else
                    {
                        NewDesign.CurrentForm?.AddLauncherLog("Standby cache clear requires admin.");
                    }
                }

                // Give the OS a moment to update memory counters
                Thread.Sleep(1500);

                float afterPercent = _ram.GetUsagePercent();
                ulong afterAvailable = _ram.GetAvailableBytes();
                long freed = (long)afterAvailable - (long)beforeAvailable;

                NewDesign.CurrentForm?.AddLauncherLog(
                    $"After:  {afterPercent:F2}% used ({FormatBytes(afterAvailable)} free)");
                NewDesign.CurrentForm?.AddLauncherLog(
                    freed >= 0
                        ? $"Approx. freed: {FormatBytes((ulong)freed)} ({beforePercent - afterPercent:F2}%)"
                        : $"Approx. change: {FormatBytes((ulong)Math.Abs(freed))} more in use ({beforePercent - afterPercent:F2}%)");
                NewDesign.CurrentForm?.AddLauncherLog(
                    $"Done — cleaned: {cleaned}, blacklisted: {ignoredBlacklist}, skipped: {ignoredSystem}");
            }
            catch (Exception ex)
            {
                NewDesign.CurrentForm?.AddLauncherLog("[Error] Failed cleaning memory: " + ex.Message);
            }
            finally
            {
                _cleanLock.Release();
            }
        }

        /// <summary>Async wrapper for UI callers.</summary>
        public Task CleanMemAsync(bool advanced = false, bool cached = true) =>
            Task.Run(() => CleanMem(advanced, cached));

        private static string FormatBytes(ulong bytes)
        {
            const double KB = 1024;
            const double MB = KB * 1024;
            const double GB = MB * 1024;

            if (bytes >= GB) return $"{bytes / GB:F2} GB";
            if (bytes >= MB) return $"{bytes / MB:F2} MB";
            if (bytes >= KB) return $"{bytes / KB:F2} KB";
            return $"{bytes} B";
        }
    }
}