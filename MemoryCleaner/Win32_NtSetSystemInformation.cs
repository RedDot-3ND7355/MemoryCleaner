using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
    public sealed class Win32_NtSetSystemInformation
    {
        private const int SystemMemoryListInformation = 80;
        private const int MemoryPurgeStandbyList = 4;

        [DllImport("ntdll.dll")]
        private static extern uint NtSetSystemInformation(int infoClass, IntPtr info, int length);

        public static void ClearStandbyCache()
        {
            if (!Win32_PrivilegeElevation.SetIncreasePrivileges("SeProfileSingleProcessPrivilege"))
                throw new InvalidOperationException("Could not enable SeProfileSingleProcessPrivilege.");

            int command = MemoryPurgeStandbyList;
            GCHandle gcHandle = GCHandle.Alloc(command, GCHandleType.Pinned);
            try
            {
                uint status = NtSetSystemInformation(
                    SystemMemoryListInformation,
                    gcHandle.AddrOfPinnedObject(),
                    sizeof(int));

                if (status != 0U)
                {
                    throw new Win32Exception(
                        Marshal.GetLastWin32Error(),
                        $"NtSetSystemInformation failed (NTSTATUS=0x{status:X8}).");
                }
            }
            finally
            {
                if (gcHandle.IsAllocated)
                    gcHandle.Free();
            }
        }
    }
}
