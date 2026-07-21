using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
    public class RAMcs
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>();
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        private static MEMORYSTATUSEX Query()
        {
            var status = new MEMORYSTATUSEX();
            if (!GlobalMemoryStatusEx(status))
                throw new InvalidOperationException("GlobalMemoryStatusEx failed.");
            return status;
        }

        /// <summary>Used physical memory percentage (0–100).</summary>
        public float GetUsagePercent()
        {
            try
            {
                return Query().dwMemoryLoad;
            }
            catch
            {
                return 0f;
            }
        }

        /// <summary>Available physical memory in bytes.</summary>
        public ulong GetAvailableBytes()
        {
            try
            {
                return Query().ullAvailPhys;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>Total physical memory in bytes.</summary>
        public ulong GetTotalBytes()
        {
            try
            {
                return Query().ullTotalPhys;
            }
            catch
            {
                return 0;
            }
        }

        // ---- Back-compat wrappers ----

        public bool IsCounters { get; set; } = true;

        public void _IsCounters() => IsCounters = true;

        public float Current_Ram() => GetAvailableBytes();

        public float Current_Usage() => GetUsagePercent();

        public float No_Counters_Curr_Usage() => GetUsagePercent();
    }
}