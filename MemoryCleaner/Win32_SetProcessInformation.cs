using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
    public class Win32_SetProcessInformation
    {
        public const int PROCESS_POWER_THROTTLING_CURRENT_VERSION = 1;
        public const int PROCESS_POWER_THROTTLING_EXECUTION_SPEED = 1;
        public const int PROCESS_POWER_THROTTLING_IGNORE_TIMER_RESOLUTION = 4;

        [DllImport("kernel32.dll")]
        public static extern int GetProcessInformation(
            IntPtr hProcess,
            PROCESS_INFORMATION_CLASS ProcessInformationClass,
            IntPtr ProcessInformation,
            int ProcessInformationSize);

        [DllImport("kernel32.dll")]
        public static extern bool SetProcessInformation(
            IntPtr hProcess,
            PROCESS_INFORMATION_CLASS ProcessInformationClass,
            IntPtr ProcessInformation,
            int ProcessInformationSize);

        public static bool SetProcessInfo(IntPtr handle, PROCESS_INFORMATION_CLASS piClass, object processInfo)
        {
            Type? infoType = GetInfoType(piClass);
            if (infoType == null)
                return false;

            int size = Marshal.SizeOf(infoType);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(processInfo, ptr, false);
                return SetProcessInformation(handle, piClass, ptr, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public static bool GetProcessInfo(IntPtr handle, PROCESS_INFORMATION_CLASS piClass, out object? processInfo)
        {
            Type? infoType = GetInfoType(piClass);
            if (infoType == null)
            {
                processInfo = null;
                return false;
            }

            int size = Marshal.SizeOf(infoType);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                int result = GetProcessInformation(handle, piClass, ptr, size);
                processInfo = Marshal.PtrToStructure(ptr, infoType);
                return result != 0;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        private static Type? GetInfoType(PROCESS_INFORMATION_CLASS piClass) => piClass switch
        {
            PROCESS_INFORMATION_CLASS.ProcessMemoryPriority => typeof(MEMORY_PRIORITY_INFORMATION),
            PROCESS_INFORMATION_CLASS.ProcessPowerThrottling => typeof(PROCESS_POWER_THROTTLING_STATE),
            _ => null
        };

        public enum PROCESS_INFORMATION_CLASS
        {
            ProcessMemoryPriority,
            ProcessMemoryExhaustionInfo,
            ProcessAppMemoryInfo,
            ProcessInPrivateInfo,
            ProcessPowerThrottling,
            ProcessReservedValue1,
            ProcessTelemetryCoverageInfo,
            ProcessProtectionLevelInfo,
            ProcessLeapSecondInfo,
            ProcessMachineTypeInfo,
            ProcessInformationClassMax
        }

        public struct PROCESS_POWER_THROTTLING_STATE
        {
            public uint Version;
            public uint ControlMask;
            public uint StateMask;

            public override string ToString() =>
                $"PROCESS_POWER_THROTTLING_STATE:\n\tVersion: {Version}\n\tControlMask:{ControlMask}\n\tStateMask:{StateMask}\n";
        }

        public struct MEMORY_PRIORITY_INFORMATION
        {
            public uint MemoryPriority;

            public override string ToString() =>
                $"MEMORY_PRIORITY_INFORMATION:\n\tMemoryPriority: {MemoryPriority}\n";
        }
    }
}
