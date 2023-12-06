using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
	// Token: 0x0200000F RID: 15
	internal class Win32_SetProcessInformation
	{
		// Token: 0x0600007A RID: 122
		[DllImport("kernel32.dll")]
		public static extern int GetProcessInformation(IntPtr hProcess, Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS ProcessInformationClass, IntPtr ProcessInformation, int ProcessInformationSize);

		// Token: 0x0600007B RID: 123
		[DllImport("kernel32.dll")]
		public static extern bool SetProcessInformation(IntPtr hProcess, Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS ProcessInformationClass, IntPtr ProcessInformation, int ProcessInformationSize);

		// Token: 0x0600007C RID: 124 RVA: 0x00003158 File Offset: 0x00001358
		public static bool SetProcessInfo(IntPtr handle, Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS piClass, object processInfo)
		{
			Type infoType = null;
			if (piClass != Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority)
			{
				if (piClass == Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling)
				{
					infoType = typeof(Win32_SetProcessInformation.PROCESS_POWER_THROTTLING_STATE);
				}
			}
			else
			{
				infoType = typeof(Win32_SetProcessInformation.MEMORY_PRIORITY_INFORMATION);
			}
			if (infoType != null)
			{
				int sizeOfProcessInfo = Marshal.SizeOf(infoType);
				IntPtr pProcessInfo = Marshal.AllocHGlobal(sizeOfProcessInfo);
				Marshal.StructureToPtr(processInfo, pProcessInfo, false);
				bool flag = Win32_SetProcessInformation.SetProcessInformation(handle, piClass, pProcessInfo, sizeOfProcessInfo);
				Marshal.FreeHGlobal(pProcessInfo);
				return flag != Convert.ToBoolean(0);
			}
			return false;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031C4 File Offset: 0x000013C4
		public static bool GetProcessInfo(IntPtr handle, Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS piClass, out object processInfo)
		{
			Type infoType = null;
			if (piClass != Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority)
			{
				if (piClass == Win32_SetProcessInformation.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling)
				{
					infoType = typeof(Win32_SetProcessInformation.PROCESS_POWER_THROTTLING_STATE);
				}
			}
			else
			{
				infoType = typeof(Win32_SetProcessInformation.MEMORY_PRIORITY_INFORMATION);
			}
			if (infoType != null)
			{
				int sizeOfProcessInfo = Marshal.SizeOf(infoType);
				IntPtr pProcessInfo = Marshal.AllocHGlobal(sizeOfProcessInfo);
				int processInformation = Win32_SetProcessInformation.GetProcessInformation(handle, piClass, pProcessInfo, sizeOfProcessInfo);
				processInfo = Marshal.PtrToStructure(pProcessInfo, infoType);
				Marshal.FreeHGlobal(pProcessInfo);
				return processInformation != 0;
			}
			processInfo = null;
			return false;
		}

		// Token: 0x0400003D RID: 61
		public const int PROCESS_POWER_THROTTLING_CURRENT_VERSION = 1;

		// Token: 0x0400003E RID: 62
		public const int PROCESS_POWER_THROTTLING_EXECUTION_SPEED = 1;

		// Token: 0x0400003F RID: 63
		public const int PROCESS_POWER_THROTTLING_IGNORE_TIMER_RESOLUTION = 4;

		// Token: 0x0200002C RID: 44
		public enum PROCESS_INFORMATION_CLASS
		{
			// Token: 0x040000D4 RID: 212
			ProcessMemoryPriority,
			// Token: 0x040000D5 RID: 213
			ProcessMemoryExhaustionInfo,
			// Token: 0x040000D6 RID: 214
			ProcessAppMemoryInfo,
			// Token: 0x040000D7 RID: 215
			ProcessInPrivateInfo,
			// Token: 0x040000D8 RID: 216
			ProcessPowerThrottling,
			// Token: 0x040000D9 RID: 217
			ProcessReservedValue1,
			// Token: 0x040000DA RID: 218
			ProcessTelemetryCoverageInfo,
			// Token: 0x040000DB RID: 219
			ProcessProtectionLevelInfo,
			// Token: 0x040000DC RID: 220
			ProcessLeapSecondInfo,
			// Token: 0x040000DD RID: 221
			ProcessMachineTypeInfo,
			// Token: 0x040000DE RID: 222
			ProcessInformationClassMax
		}

		// Token: 0x0200002D RID: 45
		public struct PROCESS_POWER_THROTTLING_STATE
		{
			// Token: 0x06000107 RID: 263 RVA: 0x00004C4C File Offset: 0x00002E4C
			public override string ToString()
			{
				return "PROCESS_POWER_THROTTLING_STATE:\n" + string.Format("\tVersion: {0}\n", this.Version) + string.Format("\tControlMask:{0}\n", this.ControlMask) + string.Format("\tStateMask:{0}\n", this.StateMask);
			}

			// Token: 0x040000DF RID: 223
			public uint Version;

			// Token: 0x040000E0 RID: 224
			public uint ControlMask;

			// Token: 0x040000E1 RID: 225
			public uint StateMask;
		}

		// Token: 0x0200002E RID: 46
		public struct MEMORY_PRIORITY_INFORMATION
		{
			// Token: 0x06000108 RID: 264 RVA: 0x00004CA2 File Offset: 0x00002EA2
			public override string ToString()
			{
				return "MEMORY_PRIORITY_INFORMATION:\n" + string.Format("\tMemoryPriority: {0}\n", this.MemoryPriority);
			}

			// Token: 0x040000E2 RID: 226
			public uint MemoryPriority;
		}
	}
}
