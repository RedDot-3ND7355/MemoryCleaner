using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
    // Token: 0x02000012 RID: 18
    public sealed class Win32_NtSetTimerResolution
    {
        // Token: 0x0600006A RID: 106
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetTimerResolution(uint DesiredResolution, bool SetResolution, out uint CurrentResolution);

        // Token: 0x0600006B RID: 107
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint ActualResolution);

        // Token: 0x0600006C RID: 108 RVA: 0x00002C98 File Offset: 0x00000E98
        public static void GetCurrentTimerResolution(out uint minimumResolution, out uint maximumResolution, out uint defaultResolution)
        {
            Win32_NtSetTimerResolution.NtQueryTimerResolution(out minimumResolution, out maximumResolution, out defaultResolution);
        }

        // Token: 0x0600006D RID: 109 RVA: 0x00002CA4 File Offset: 0x00000EA4
        public static void SetMaxTimerResolution(uint WantedResolution)
        {
            uint actual;
            Win32_NtSetTimerResolution.NtSetTimerResolution(WantedResolution, true, out actual);
        }

        // Token: 0x0600006E RID: 110 RVA: 0x00002CBC File Offset: 0x00000EBC
        public static void ReleaseMaxTimerResolution(uint WantedResolution)
        {
            uint actual;
            Win32_NtSetTimerResolution.NtSetTimerResolution(WantedResolution, false, out actual);
        }
    }
}
