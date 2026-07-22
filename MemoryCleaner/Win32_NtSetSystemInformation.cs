using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
    public sealed class Win32_NtSetSystemInformation
    {
        [DllImport("ntdll.dll")]
        private static extern uint NtSetSystemInformation(int infoClass, IntPtr info, int length);

        public static void ClearStandbyCache()
        {
            if (!Win32_PrivilegeElevation.SetIncreasePrivileges("SeProfileSingleProcessPrivilege"))
                return;

            try
            {
                int command = 4;
                GCHandle gcHandle = GCHandle.Alloc(command, GCHandleType.Pinned);
                try
                {
                    uint status = NtSetSystemInformation(
                        80,
                        gcHandle.AddrOfPinnedObject(),
                        sizeof(int));
                }
                finally
                {
                    if (gcHandle.IsAllocated)
                        gcHandle.Free();
                }
            }
            catch { }
        }
    }
}
